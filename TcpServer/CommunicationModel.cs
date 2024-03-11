using System;

namespace TcpServer
{
    public class CommunicationModel
    {
        public string GetGreeting()
        {
            DateTime currentTime = DateTime.Now;
            int hour = currentTime.Hour;

            if (hour < 12)
                return "Доброе утро";
            else if (hour < 17)
                return "Добрый день";
            else
                return "Добрый вечер";
        }

        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
