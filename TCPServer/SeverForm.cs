using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPServer 
{
    public partial class SeverForm : Form
    {
        private TcpListener server; // Đối tượng lắng nghe kết nối TCP từ các client
        private Thread serverThread; // Luồng chạy nền để server hoạt động song song với giao diện
        private bool isRunning = false; // Cờ kiểm soát trạng thái server
        private List<TcpClient> clients = new List<TcpClient>(); // Danh sách các client đang kết nối đến server

        public SeverForm()
        {
            InitializeComponent();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            // Không cần làm gì, chỉ để Designer load được form
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo một thread mới để chạy server(tránh làm đứng giao diện UI)
                serverThread = new Thread(StartServer);

                // Cho thread chạy nền (khi tắt chương trình, thread sẽ tự dừng)
                serverThread.IsBackground = true;

                // Bắt đầu chạy thread => gọi hàm StartServer()
                serverThread.Start();

                // Ghi log ra ListBox (UI)
                Log("Server thread started.");
                // Cập nhật nhãn trạng thái trên giao diện
                lblStatus.Text = "Server is running...";
            }
            catch (Exception ex)
            {
                // Nếu có lỗi khi khởi động server thì ghi thông tin lỗi ra log
                Log("Error starting server: " + ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                // Đặt cờ isRunning = false để dừng vòng lặp chấp nhận client mới
                isRunning = false;

                // Dừng listener (không lắng nghe kết nối mới nữa)
                server?.Stop();

                // Đóng tất cả client đang kết nối
                foreach (var c in clients)
                    c.Close();

                // Xóa danh sách client trong bộ nhớ
                clients.Clear();
                // Xóa danh sách hiển thị trên UI (ListBox lstClients)
                lstClients.Items.Clear();

                // Ghi log và cập nhật trạng thái
                Log("Server stopped.");
                lblStatus.Text = "Server stopped";
            }
            catch (Exception ex)
            {
                // Nếu có lỗi khi dừng server, ghi lại để kiểm tra
                Log("Error stopping server: " + ex.Message);
            }
        }

        private void StartServer()
        {
            try
            {
                // Cấu hình thông tin địa chỉ IP và cổng mà server sẽ lắng nghe
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                int port = 8888;

                // Tạo đối tượng TcpListener để lắng nghe kết nối TCP
                server = new TcpListener(localAddr, port);

                // Bắt đầu lắng nghe (bật chế độ listen)
                server.Start();

                // Đặt cờ điều khiển, báo rằng server đang chạy
                isRunning = true;

                // Ghi log ra giao diện thông báo server đã sẵn sàng
                Log($"Server started on {localAddr}:{port}");

                // Vòng lặp chính: liên tục chấp nhận client mới
                while (isRunning)
                {
                    TcpClient client = server.AcceptTcpClient();
                    clients.Add(client); // Thêm client vào danh sách

                    Log("Client connected: " + client.Client.RemoteEndPoint);

                    // Cập nhật giao diện lstClients
                    lstClients.Invoke(new Action(() =>
                        lstClients.Items.Add(client.Client.RemoteEndPoint.ToString())));

                    // Tạo thread riêng cho client
                    Thread clientThread = new Thread(() =>
                    {
                        ClientHandler handler = new ClientHandler(client, Log);
                        handler.HandleClient();

                        // Khi client ngắt kết nối
                        clients.Remove(client);
                        lstClients.Invoke(new Action(() =>
                            lstClients.Items.Remove(client.Client.RemoteEndPoint.ToString())));
                    });

                    // Đặt thread chạy nền để tự dừng khi tắt chương trình
                    clientThread.IsBackground = true;

                    // Khởi chạy thread mới cho client
                    clientThread.Start();
                }
            }
            catch (SocketException ex)
            {
                Log("Socket error: " + ex.Message);
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;

            // Lấy luồng dữ liệu (stream) để gửi/nhận dữ liệu qua kết nối TCP
            NetworkStream stream = client.GetStream();

            // Tạo mảng byte làm bộ đệm tạm để chứa dữ liệu nhận từ client
            byte[] buffer = new byte[1024];

            // Biến lưu số byte thật sự đọc được từ client trong mỗi lần đọc
            int bytesRead;

            try
            {
                // Vòng lặp chính để liên tục nhận dữ liệu từ client
                // stream.Read() sẽ chờ cho đến khi client gửi dữ liệu
                // hoặc trả về 0 nếu client đóng kết nối
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    // Chuyển byte[] nhận được thành chuỗi UTF-8
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Ghi log lại dữ liệu server vừa nhận được
                    Log("Received: " + data);

                    // Echo lại cho client
                    byte[] msg = Encoding.UTF8.GetBytes("Server received: " + data);
                    stream.Write(msg, 0, msg.Length);
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi ghi log để biết nguyên nhân
                Log("Client disconnected or error: " + ex.Message);
            }
            finally
            {
                // Dù có lỗi hay client đóng kết nối, vẫn cần đóng socket để giải phóng tài nguyên
                client.Close();
            }
        }

        private void Log(string message)
        {
            // Gọi Invoke vì thread socket không thể cập nhật UI trực tiếp
            if (lstLog.InvokeRequired)
            {
                // Gọi Invoke để thêm dòng log vào ListBox từ thread giao diện (UI thread)
                lstLog.Invoke(new Action(() => lstLog.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}")));
            }
            else
            {
                // Nếu đang ở đúng thread giao diện thì thêm log trực tiếp
                lstLog.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            }
        }

        private void lstLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không cần làm gì, chỉ để Designer load được form
        }

        private void lblClients_Click(object sender, EventArgs e)
        {
            // Không cần làm gì, chỉ để Designer load được form
        }

        private void lstClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không cần làm gì, chỉ để Designer load được form
        }
    }
}
