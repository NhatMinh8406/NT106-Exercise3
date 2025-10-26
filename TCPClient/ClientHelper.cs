using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpUserServer;

namespace TCPClient
{
    public static class ClientHelper
    {
        public static string ServerHost = "127.0.0.1";
        public static int ServerPort = 8888; // Đảm bảo cổng này khớp với Server

        public static async Task<Response> SendRequestAsync(Request req)
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync(ServerHost, ServerPort);
            try
            {
                // Dùng StreamReader/Writer để khớp với Server
                using (NetworkStream stream = client.GetStream())
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    // 1. GỬI YÊU CẦU (SEND)
                    string reqJson = JsonConvert.SerializeObject(req);

                    // Gửi đi 1 dòng (tự thêm \n)
                    await writer.WriteLineAsync(reqJson);
                    await writer.FlushAsync();

                    // 2. NHẬN PHẢN HỒI (RECEIVE)
                    // Đọc về 1 dòng (chờ đến khi gặp \n)
                    string respLine = await reader.ReadLineAsync();

                    if (string.IsNullOrEmpty(respLine))
                    {
                        return new Response { Success = false, Message = "No response from server" };
                    }

                    // Deserialize chuỗi thành object
                    Response resp = JsonConvert.DeserializeObject<Response>(respLine);
                    return resp;
                }
            }
            finally
            {
                try { client.Close(); } catch { }
            }
        }
    }
}