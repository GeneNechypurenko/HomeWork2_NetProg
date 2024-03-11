using System.Net.Sockets;
using System.Net;
using System.Text;

namespace HomeWork2
{

    public class CommunicationModel
    {
        public string GetGreeting()
        {
            DateTime currentTime = DateTime.Now;
            int hour = currentTime.Hour;

            if (hour < 12)
                return "Доброго ранку";
            else if (hour < 17)
                return "Доброго дня";
            else
                return "Доброго вечора";
        }

        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }

    // Контролер
    public class CommunicationController
    {
        private CommunicationModel model;

        public CommunicationController(CommunicationModel model)
        {
            this.model = model;
        }

        public string ProcessClientRequest(string request)
        {
            if (request.Equals("Привіт", StringComparison.OrdinalIgnoreCase))
            {
                return model.GetGreeting();
            }
            else if (request.Equals("Час", StringComparison.OrdinalIgnoreCase))
            {
                return model.GetCurrentTime();
            }

            return "Невідомий запит";
        }
    }

    // TCP сервер
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
            Console.WriteLine("Сервер запущено. Очікування клієнтів...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                HandleClient(client);
            }
        }

        private void HandleClient(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string clientRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Запит від клієнта: {clientRequest}");

            string response = controller.ProcessClientRequest(clientRequest);
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            stream.Write(responseData, 0, responseData.Length);

            tcpClient.Close();
        }
    }

    // TCP клієнт (може бути окремим проектом або консольним застосунком)
    class TcpClientProgram
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 12345); // Змініть IP-адресу та порт на власні потреби

            NetworkStream stream = client.GetStream();

            Console.Write("Введіть запит: ");
            string request = Console.ReadLine();

            byte[] requestData = Encoding.UTF8.GetBytes(request);
            stream.Write(requestData, 0, requestData.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Відповідь від сервера: {response}");

            client.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CommunicationModel model = new CommunicationModel();
            CommunicationController controller = new CommunicationController(model);
            TcpServer server = new TcpServer(12345, controller);

            server.Start();
        }
    }
}
