using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    public class ClientSocket
    {
        private TcpClient client;
        private NetworkStream stream;

        public bool IsConnected => client?.Connected ?? false;

        public async Task<bool> ConnectAsync(string host, int port)
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(host, port);
                stream = client.GetStream();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task SendAsync(string message)
        {
            if (stream == null) return;
            byte[] data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);
        }

        public async Task<string> ReceiveAsync()
        {
            if (stream == null) return null;
            byte[] buffer = new byte[4096];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        public void Close()
        {
            stream?.Close();
            client?.Close();
        }
    }
}
