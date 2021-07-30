using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using ApiBillableTransaction.TransactionManager;
using System.Text.Json;

namespace ApiBillableTransaction.Messaging
{
    public class SendTransaction :ISendTransaction
    {
        private string host;
        private string userName;
        private string password;
        private string queueName;
        private int port;
        private IConnection connection;
        public SendTransaction(IOptions<RabbitMQConfiguration> optionsRabbitMQ)
        {
            host = optionsRabbitMQ.Value.HostName;
            userName = optionsRabbitMQ.Value.UserName;
            password = optionsRabbitMQ.Value.Password;
            queueName = optionsRabbitMQ.Value.QueueName;
            port = optionsRabbitMQ.Value.Port;
        }

        public void SendTransactionMsg(Movements movements)
        {
            if (ConnectionExists())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                     
                    var json = JsonSerializer.Serialize(movements);
                    var body = System.Text.Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
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
                    HostName = host,
                    UserName = userName,
                    Password = password,
                    Port = port
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
