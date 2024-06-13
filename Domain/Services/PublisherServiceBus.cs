using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PublisherServiceBus : IPublisherServiceBus
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public PublisherServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMq:Host"],
                //Port = int.Parse(_configuration["RabbitMq:Port"])
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        private void _connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connection has been shutdown");
        }

        public void PublishMessage<T>(T model, string exchangeName, string queueName, string routingKey)
        {
            var message = JsonSerializer.Serialize(model);
            if (_connection is not null)
            {
                if (_connection.IsOpen)
                {
                    Console.WriteLine("RabbitMQ Connection Is Open");
                    var result = SendMessage(message, exchangeName, queueName, routingKey);
                    if (result is true)
                    {
                        Console.WriteLine($"{message} has been sent");
                    }
                }
            }
        }

        private bool SendMessage(string message, string exchangeName, string queueName, string routingKey)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);
                ConnectAndDeclareExchangeWithQueue(exchangeName, queueName, routingKey);

                _channel.BasicPublish(exchangeName, routingKey, null, body);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ConnectAndDeclareExchangeWithQueue(string exchangeName, string queueName, string routingKey)
        {
            try
            {
                _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
                _channel.QueueDeclare(queueName, false, false, false, null);
                _channel.QueueBind(queueName, exchangeName, routingKey, null);
                _connection.ConnectionShutdown += _connection_ConnectionShutdown;
                Console.WriteLine("Connection has been created");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to the rabbitmq: {ex.Message}");
            }
        }
    }
}
