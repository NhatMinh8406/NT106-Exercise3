using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    class ProtocolHandler
    {
        // Tạo một đối tượng (instance) của lớp Database
        // Điều này cho phép ProtocolHandler gọi các hàm trong Database.cs
        private Database db;

        // Constructor: Được gọi khi ClientHandler tạo đối tượng này
        public ProtocolHandler()
        {
            db = new Database();
        }

        // Hàm xử lý Request chính
        // Hàm này nhận 1 chuỗi request (ví dụ: "LOGIN|user|pass")
        // Và trả về 1 chuỗi response (ví dụ: "LOGIN_SUCCESS|...")
        public string ProcessRequest(string request)
        {
            string response = "INVALID_REQUEST|Yêu cầu không hợp lệ"; // Phản hồi mặc định

            try
            {
                // Tách chuỗi request bằng dấu '|'
                // Ví dụ: "REGISTER|user1|pass|email|name|bday"
                string[] parts = request.Split('|');
                string command = parts[0]; // Lấy lệnh (ví dụ: "REGISTER", "LOGIN")

                // Dùng switch-case để xử lý theo lệnh
                switch (command)
                {
                    // Trường hợp xử lý Đăng ký
                    case "REGISTER":
                        // "REGISTER"|[1]username|[2]hashed_pass|[3]email|[4]fullname|[5]birthday
                        if (parts.Length >= 6)
                        {
                            string username = parts[1];
                            string password = parts[2];
                            string email = parts[3];
                            string fullName = parts[4];
                            string birthday = parts[5];

                            //  Kiểm tra username tồn tại chưa
                            if (db.CheckUsernameExists(username))
                            {
                                response = "REGISTER_FAIL|Username đã tồn tại";
                            }
                            else
                            {
                                // Thêm user mới
                                bool success = db.AddNewUser(username, password, email, fullName, birthday);
                                if (success)
                                {
                                    response = "REGISTER_SUCCESS|Đăng ký thành công";
                                }
                                else
                                {
                                    response = "REGISTER_FAIL|Lỗi khi thêm vào CSDL (có thể trùng email)";
                                }
                            }
                        }
                        break;

                    // Trường hợp xử lý Đăng nhập
                    case "LOGIN":
                        // "LOGIN"|[1]username|[2]hashed_pass
                        if (parts.Length >= 3)
                        {
                            string username = parts[1];
                            string password = parts[2];

                            // Kiểm tra đăng nhập
                            User user = db.ValidateLogin(username, password);
                            if (user != null)
                            {
                                // Đăng nhập thành công!
                                // Gửi về thông tin user (FullName, Email, Birthday)
                                // Chuyển đổi Birthday (kiểu DateTime?) sang chuỗi
                                string bdayStr = user.Birthday?.ToString("yyyy-MM-dd") ?? "Chưa có";

                                // Format: "LOGIN_SUCCESS"|FullName|Email|Birthday
                                response = $"LOGIN_SUCCESS|{user.FullName}|{user.Email}|{bdayStr}";
                            }
                            else
                            {
                                // Đăng nhập thất bại
                                response = "LOGIN_FAIL|Sai tên đăng nhập hoặc mật khẩu";
                            }
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ProtocolHandler: " + ex.Message);
                response = "ERROR|Có lỗi xảy ra phía server";
            }

            return response;
        }
    }
}