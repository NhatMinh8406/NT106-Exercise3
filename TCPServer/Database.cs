using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; 
using System.Data;          

namespace TCPServer
{

    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; } 
    }

    class Database
    {
        private string connectionString = "Data Source=DBAO\\SQLEXPRESS;Initial Catalog=QuanLyNguoiDungBai3NT106;User ID=user;Password=user123;Persist Security Info=True";

        //Hàm kiểm tra Username đã tồn tại chưa
        public bool CheckUsernameExists(string username)
        {
            // Dùng 'using' để đảm bảo kết nối được đóng tự động, ngay cả khi có lỗi
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Luôn dùng Parameter (@Username) để chống lỗi SQL Injection
                    cmd.Parameters.AddWithValue("@Username", username);

                    // ExecuteScalar dùng khi truy vấn chỉ trả về 1 giá trị duy nhất (là số lượng)
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Nếu count > 0, tức là username đã tồn tại
                }
            }
        }

        // Hàm thêm User mới (dùng cho Đăng ký)
        public bool AddNewUser(string username, string hashedPassword, string email, string fullName, string birthday)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Câu lệnh INSERT
                string query = @"INSERT INTO Users (Username, Password, Email, FullName, Birthday) 
                                 VALUES (@Username, @Password, @Email, @FullName, @Birthday)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm các parameter
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword); // Mật khẩu đã hash
                    cmd.Parameters.AddWithValue("@Email", email);

                    // Xử lý các giá trị có thể là NULL (vì FullName và Birthday cho phép NULL)
                    if (string.IsNullOrEmpty(fullName))
                        cmd.Parameters.AddWithValue("@FullName", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@FullName", fullName);

                    if (string.IsNullOrEmpty(birthday))
                        cmd.Parameters.AddWithValue("@Birthday", DBNull.Value);
                    else
                    {
                        // Cố gắng chuyển đổi chuỗi ngày sinh sang kiểu DateTime
                        if (DateTime.TryParse(birthday, out DateTime bday))
                            cmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = bday;
                        else
                            cmd.Parameters.AddWithValue("@Birthday", DBNull.Value); // Nếu parse lỗi, gửi NULL
                    }

                    try
                    {
                        // ExecuteNonQuery dùng cho INSERT, UPDATE, DELETE. Nó trả về số dòng bị ảnh hưởng
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Nếu > 0 là insert thành công
                    }
                    catch (SqlException ex)
                    {
                        // Lỗi (thường là do vi phạm ràng buộc UNIQUE của Username hoặc Email)
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }

        // Hàm kiểm tra đăng nhập
        // Trả về một đối tượng User nếu thành công, trả về null nếu thất bại
        public User ValidateLogin(string username, string hashedPassword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // không lấy mật khẩu
                string query = "SELECT FullName, Email, Birthday FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    // ExecuteReader dùng khi truy vấn trả về một tập kết quả (dù chỉ là 1 hàng)
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Nếu tìm thấy một hàng (tức là đăng nhập đúng)
                        {
                            User user = new User();

                            // Xử lý DBNull cho các cột nullable
                            user.FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : null;
                            user.Email = reader["Email"].ToString(); // Email là NOT NULL
                            user.Birthday = reader["Birthday"] != DBNull.Value ? (DateTime?)reader["Birthday"] : null;

                            return user;
                        }
                    }
                }
            }

            return null; // Không tìm thấy user or sai mật khẩu
        }
    }
}
