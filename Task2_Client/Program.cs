using System.Net.Sockets;
using System.Text;

namespace Task2_TaskManagerClient
{
    internal class Program
    {
        static void Main()
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 12345))
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string processList = Encoding.UTF8.GetString(data, 0, bytesRead);

                Console.WriteLine("Received process list:");
                Console.WriteLine(processList);
            }
        }
    }
}
