using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    public class TcpServer
    {
        private TcpListener listener;
        private CommunicationController controller;

        public TcpServer(int port, CommunicationController controller)
        {
            listener = new TcpListener(IPAddress.Any, port);
            this.controller = controller;
        }

        public void Start()
        {
            listener.Start();
            Console.WriteLine("Сервер запущен. Ожидание клиентов...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => { HandleClient(client); });
            }
        }

        private void HandleClient(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();

            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string clientRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Запрос клиента: {clientRequest}");

                    if (clientRequest.Equals("Пока", StringComparison.OrdinalIgnoreCase))
                        break;

                    string response = controller.ProcessClientRequest(clientRequest);
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);
                }
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}
