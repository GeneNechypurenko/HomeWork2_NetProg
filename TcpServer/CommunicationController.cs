namespace TcpServer
{
    public class CommunicationController
    {
        private CommunicationModel model;

        public CommunicationController(CommunicationModel model)
        {
            this.model = model;
        }

        public string ProcessClientRequest(string request)
        {
            if (request.Equals("Привет", StringComparison.OrdinalIgnoreCase))
            {
                return model.GetGreeting();
            }
            else if (request.Equals("Время", StringComparison.OrdinalIgnoreCase))
            {
                return model.GetCurrentTime();
            }

            return "Неизвестный запрос";
        }
    }
}