using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    class ProtocolHandler
    {
        private Database db;

        public ProtocolHandler()
        {
            db = new Database();
        }

        // Hàm này giờ sẽ nhận JSON và trả về JSON
        public string ProcessRequest(string requestJson)
        {
            Response resp; // Đối tượng phản hồi

            try
            {
                // 1. Deserialize chuỗi JSON nhận được thành đối tượng Request
                Request req = JsonConvert.DeserializeObject<Request>(requestJson);

                // 2. Chuyển req.Data (kiểu object) thành JObject để dễ truy cập
                JObject data = JObject.FromObject(req.Data);

                // 3. Xử lý dựa trên 'Action'
                switch (req.Action)
                {
                    case "Register": // Tên Action từ Client
                        resp = HandleRegister(data);
                        break;

                    case "Login": // Tên Action từ Client
                        resp = HandleLogin(data);
                        break;

                    default:
                        resp = new Response { Success = false, Message = "Hành động không xác định" };
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ProtocolHandler: " + ex.Message);
                resp = new Response { Success = false, Message = "Lỗi phía server: " + ex.Message };
            }

            // 4. Serialize đối tượng Response thành chuỗi JSON để gửi về client
            return JsonConvert.SerializeObject(resp);
        }

        // === ĐÂY LÀ HÀM QUAN TRỌNG BẠN HỎI Ở MỤC 4 ===
        // Hàm xử lý Đăng nhập
        private Response HandleLogin(JObject data)
        {
            // Lấy dữ liệu từ JObject (khớp với tên Client gửi lên)
            string username = data.Value<string>("Username");
            string password = data.Value<string>("Password"); // Tên đã sửa là 'Password'

            User user = db.ValidateLogin(username, password);

            if (user != null)
            {
                // Đăng nhập thành công
                // Client (LoginForm) mong đợi một đối tượng Data chứa Token và User
                var responseData = new
                {
                    Token = Guid.NewGuid().ToString(), // Tạo 1 token ngẫu nhiên
                    User = user // Gửi về toàn bộ đối tượng User (FullName, Email, Birthday)
                };

                return new Response
                {
                    Success = true,
                    Message = "Đăng nhập thành công!",
                    Data = JObject.FromObject(responseData) // Chuyển thành JObject
                };
            }
            else
            {
                // Đăng nhập thất bại
                return new Response { Success = false, Message = "Sai tên đăng nhập hoặc mật khẩu" };
            }
        }

        // Hàm xử lý Đăng ký
        private Response HandleRegister(JObject data)
        {
            // Lấy dữ liệu (khớp với tên Client gửi lên)
            string username = data.Value<string>("Username");
            string password = data.Value<string>("Password"); 
            string email = data.Value<string>("Email");
            string fullName = data.Value<string>("FullName");
            string birthday = data.Value<string>("Birthday"); 

            if (db.CheckUsernameExists(username))
            {
                return new Response { Success = false, Message = "Username đã tồn tại" };
            }

            bool success = db.AddNewUser(username, password, email, fullName, birthday);
            if (success)
            {
                return new Response { Success = true, Message = "Đăng ký thành công" };
            }
            else
            {
                return new Response { Success = false, Message = "Lỗi khi thêm vào CSDL (có thể trùng email)" };
            }
        }
    }


    public class Request
    {
        public string Action { get; set; }
        public object Data { get; set; } 
    }

    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public JObject Data { get; set; } 
    }
}