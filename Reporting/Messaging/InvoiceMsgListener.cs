using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Reporting.Data.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using Serilog;
using Reporting.Services;

namespace Reporting.Messaging
{
    public class InvoiceMsgListener : BackgroundService
    {
        public string Host { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string QueueName { set; get; }
        
        private IConnection connection;
        private IModel channel;
        private readonly  IInvoiceService _invoiceService;
        
        public InvoiceMsgListener(IOptions<RabbitMQConfiguration> optionsRabbitMQ, IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;

            Host = optionsRabbitMQ.Value.HostName;
            UserName = optionsRabbitMQ.Value.UserName;
            Password = optionsRabbitMQ.Value.Password;
            QueueName = optionsRabbitMQ.Value.QueueName;
            
            CreateConnection();

            if(connection == null)
            {
                Log.Debug("Null Connection");
                return;
            }

            try
            {
                channel = connection.CreateModel();
                channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            }catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }
        protected override Task ExecuteAsync(CancellationToken token)
        {
            var consumer = new EventingBasicConsumer(channel); 
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var invoice = JsonSerializer.Deserialize<Invoice>(content);

                if(invoice == null)
                {
                    return;
                }

                _invoiceService.CreateInvoice(invoice);

                channel.BasicAck(ea.DeliveryTag, false);
            };
            
            channel.BasicConsume(QueueName, false, consumer);
           
            return Task.CompletedTask;
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {

                    HostName = Host,
                    UserName = ConnectionFactory.DefaultUser,
                    Password = ConnectionFactory.DefaultPass,
                    Port = AmqpTcpEndpoint.UseDefaultPort
                };
                connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Log.Error($"Could not create connection: {ex.Message}");
            }
        }
    }
}
