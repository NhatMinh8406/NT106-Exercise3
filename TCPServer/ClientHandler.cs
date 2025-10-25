using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCPServer;

namespace TCPServer
{
     public class ClientHandler
    {
        private TcpClient client; // Đại diện cho 1 kết nối TCP từ client
        private Action<string> Log; // delegate để ghi log lên UI
        private bool isConnected = true; // Cờ để kiểm soát trạng thái kết nối

        // Hàm khởi tạo: nhận TcpClient (đã được server chấp nhận) và delegate để ghi log
        public ClientHandler(TcpClient tcpClient, Action<string> logAction)
        {
            client = tcpClient;
            Log = logAction;
        }

        // Phương thức chính: xử lý giao tiếp với client
        public void HandleClient()
        {
            try
            {
                // Tạo stream để đọc/ghi dữ liệu qua socket
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[4096];

                while (isConnected)
                {
                    // Đọc dữ liệu từ client gửi đến
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // client ngắt kết nối

                    // Giải mã byte[] thành chuỗi UTF8
                    string requestJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Ghi log nội dung nhận được cùng địa chỉ IP client
                    Log($"Received from {client.Client.RemoteEndPoint}: {requestJson}");

                    // Giải mã JSON yêu cầu
                    string response = HandleRequest(requestJson);

                    // Gửi phản hồi lại client
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi trong khi nhận/gửi dữ liệu → ghi log lỗi
                Log($"Client error: {ex.Message}");
            }
            finally
            {
                // Khi client ngắt kết nối hoặc có lỗi → đóng kết nối và ghi log
                Log($"Client disconnected: {client.Client.RemoteEndPoint}");
                client.Close();
            }
        }

        private string HandleRequest(string json)
        {

            try
            {
                // Giao cho ProtocolHandler xử lý
                ProtocolHandler handler = new ProtocolHandler();
                return handler.ProcessRequest(json);
            }
            catch (Exception ex)
            {
                // Nếu xảy ra lỗi, trả về phản hồi JSON báo lỗi
                return JsonConvert.SerializeObject(new { status = "error", message = ex.Message });
            }

        }
    }
}
