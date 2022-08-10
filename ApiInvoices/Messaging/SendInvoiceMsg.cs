using System;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using System.Text.Json;
using ApiInvoices.Messaging;
using ApiInvoices.Data;

namespace ApiBillableTransaction.Messaging
{
    public class SendInvoiceMsg : ISendInvoiceMsg
    {
        private readonly string Host;
        private readonly string UserName;
        private readonly string Password;
        private readonly string QueueName;
        private readonly int Port;

        private IConnection connection;
        public SendInvoiceMsg(IOptions<RabbitMQConfigurationReporting> optionsRabbitMQ)
        {
            Host = optionsRabbitMQ.Value.HostName;
            UserName = optionsRabbitMQ.Value.UserName;
            Password = optionsRabbitMQ.Value.Password;
            QueueName = optionsRabbitMQ.Value.QueueName;
            Port = optionsRabbitMQ.Value.Port;
        }

        public void SendMsg(Invoice invoice)
        {
            if (ConnectionExists())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                     
                    var json = JsonSerializer.Serialize(invoice);
                    var body = System.Text.Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
                }
            }
        }

        private bool ConnectionExists()
        {
            if (connection != null)
            {
                return true;
            }

            CreateConnection();

            return connection != null;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = Host,
                    UserName = UserName,
                    Password = Password,
                    Port = Port
                };
                connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }
    }
}
