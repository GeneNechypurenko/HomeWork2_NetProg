using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace Task2_TaskManagerTcpServer
{
    internal class Program
    {
        static void Main()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 12345);
            server.Start();
            Console.WriteLine("Server started. Waiting for clients...");

            while (true)
            {
                using (TcpClient client = server.AcceptTcpClient())
                using (NetworkStream stream = client.GetStream())
                {
                    string processList = GetRunningProcesses();
                    byte[] data = Encoding.UTF8.GetBytes(processList);
                    stream.Write(data, 0, data.Length);
                }
            }
        }
        private static string GetRunningProcesses()
        {
            Process[] processes = Process.GetProcesses();
            StringBuilder sb = new StringBuilder();

            foreach (Process process in processes)
            {
                sb.AppendLine(process.ProcessName);
            }
            return sb.ToString();
        }
    }
}
