namespace ApiInvoices.Messaging
{
    public class RabbitMQConfiguration
    {
        public string HostName { set; get; }
        public string QueueName { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public int Port { set; get; }
    }
    public class RabbitMQConfigurationReporting : RabbitMQConfiguration
    {
    }
}
