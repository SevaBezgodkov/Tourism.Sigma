using Domain.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RepositoryService.Background.Interfaces;
using RepositoryService.Command;
using RepositoryService.Command.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace RepositoryService.Background
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly ITasksHandler _handler;
        private IModel _channel;

        public RabbitMqConsumer(IConfiguration configuration, ITasksHandler handler)
        {
            _configuration = configuration;
            _handler = handler;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMq:Host"],
                //Port = int.Parse(_configuration["RabbitMq:Port"])
            };
            try
            {
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ReceiveMessages()
        {
            InitializeRabbitMqConfiguration();

            var consumer = new EventingBasicConsumer(_channel);
            var queueNameBuilder = new StringBuilder();
            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = e.RoutingKey;

                var rabbitFieldsModel = JsonConvert.DeserializeObject<RabbitFieldsModel>(message);

                var queueName = rabbitFieldsModel.QueueName;

                queueNameBuilder.Append(queueName);

                await _handler.InitializeStartTaskDictionary(routingKey, message);

                _channel.BasicConsume(queue: queueNameBuilder.ToString(),
                                  autoAck: true,
                                  consumer: consumer);
            };

            _channel.BasicConsume(queue: "userQueue",
                  autoAck: true,
                  consumer: consumer);
        }

        private void InitializeRabbitMqConfiguration()
        {
            AddDirectExchangeWithQueueBinding("userQueue", "userExchange", "user.add");
            AddDirectExchangeWithQueueBinding("userQueue", "userExchange", "user.update");
        }

        private void AddDirectExchangeWithQueueBinding(string queueName, string exchangeName, string routingKey)
        {
            try
            {
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
                _channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                _channel.QueueBind(queueName, exchangeName, routingKey);

                Console.WriteLine("Connection has been created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
