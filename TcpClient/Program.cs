using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 12345);

            try
            {
                NetworkStream stream = client.GetStream();

                Task.Run(async () =>
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Ответ сервера: {response}");
                    }
                });

                while (true)
                {
                    Thread.Sleep(5);
                    Console.Write("Введите запрос: ");
                    string request = Console.ReadLine();

                    byte[] requestData = Encoding.UTF8.GetBytes(request);
                    await stream.WriteAsync(requestData, 0, requestData.Length);

                    if (request.Equals("Пока", StringComparison.OrdinalIgnoreCase))
                        break;
                }
            }
            finally
            {
                client.Close();
            }
        }
    }
}