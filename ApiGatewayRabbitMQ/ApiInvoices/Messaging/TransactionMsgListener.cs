using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ApiInvoices.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using ApiInvoices.InvoiceManager;
using ApiInvoices.Services;


namespace ApiInvoices.Messaging
{
    public class TransactionMsgListener : BackgroundService
    {
        public string host { set; get; }
        public string userName { set; get; }
        public string password { set; get; }
        public string queueName { set; get; }
        private IConnection connection;
        private IModel channel;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly  IUpdateTransactionInInvoices updateTransactionInInvoices;
        public TransactionMsgListener(IOptions<RabbitMQConfiguration> optionsRabbitMQ,
                              IInvoiceRepository _invoiceRepository,
                              IUpdateTransactionInInvoices _updateTransactionInInvoices)
        {
            host = optionsRabbitMQ.Value.HostName;
            userName = optionsRabbitMQ.Value.UserName;
            password = optionsRabbitMQ.Value.Password;
            queueName = optionsRabbitMQ.Value.QueueName;
            
            CreateConnection();
            connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            updateTransactionInInvoices = _updateTransactionInInvoices;
        }
        protected override Task ExecuteAsync(CancellationToken token)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var transaction = JsonSerializer.Deserialize<Movements>(content);

                updateTransactionInInvoices.UpdateTransaction(transaction);

                channel.BasicAck(ea.DeliveryTag, false);
            };
            channel.BasicConsume(queueName, false, consumer); 
            return Task.CompletedTask;
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {

                    HostName = host,
                    UserName = ConnectionFactory.DefaultUser,
                    Password = ConnectionFactory.DefaultPass,
                    Port = AmqpTcpEndpoint.UseDefaultPort
                };
                connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

    }
}
