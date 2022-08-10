namespace ApiDomain.Messaging;

public class BrokerConnectionProperties
{
    public string HostName { set; get; }
    public string QueueName { set; get; }
    public string UserName { set; get; }
    public string Password { set; get; }
    public int Port { set; get; }
}