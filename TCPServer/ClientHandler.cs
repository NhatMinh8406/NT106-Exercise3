using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            // Dùng StreamReader/Writer để khớp với ClientHelper
            try
            {
                // 'using' sẽ tự động đóng stream/reader/writer khi kết thúc
                using (var stream = client.GetStream())
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    while (isConnected)
                    {
                        // 1. ĐỌC DỮ LIỆU TỪ CLIENT
                        // Dùng ReadLine() thay vì stream.Read()
                        string requestJson = reader.ReadLine();

                        // Nếu client ngắt kết nối, requestJson sẽ là null
                        if (requestJson == null)
                        {
                            isConnected = false; // Đặt cờ để thoát vòng lặp
                            break; // Thoát vòng lặp
                        }

                        Log($"Received from {client.Client.RemoteEndPoint}: {requestJson}");

                        // 2. XỬ LÝ YÊU CẦU
                        string response = HandleRequest(requestJson);

                        // 3. GỬI PHẢN HỒI LẠI CLIENT
                        // Dùng WriteLine() thay vì stream.Write()
                        // WriteLine() sẽ tự động thêm ký tự '\n'
                        writer.WriteLine(response);

                        // Flush để đảm bảo dữ liệu được gửi đi ngay lập tức
                        writer.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                // Lọc lỗi "forcibly closed" khi client tắt đột ngột
                if (!(ex is IOException || ex is ObjectDisposedException))
                {
                    Log($"Client error: {ex.Message}");
                }
            }
            finally
            {
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