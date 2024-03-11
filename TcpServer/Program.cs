using System;

namespace TcpServer
{
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
