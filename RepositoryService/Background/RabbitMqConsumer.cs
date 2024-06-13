using Domain.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RepositoryService.Background.Interfaces;
using RepositoryService.Command;
using RepositoryService.Command.Interfaces;
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
        private readonly IBackgroundHandler _handler;
        private readonly ICommandUserRepository _commandUserRepository;
        private IModel _channel;

        private Dictionary<string, Func<object, Task>> startTask;

        public RabbitMqConsumer(IConfiguration configuration, IBackgroundHandler handler, ICommandUserRepository commandUserRepository)
        {
            _configuration = configuration;
            _handler = handler;
            _commandUserRepository = commandUserRepository;

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

            InitializeMethods();
        }

        private void InitializeMethods()
        {
            startTask = new Dictionary<string, Func<object, Task>>()
            {
                {"user.add", async message => 
                    {

                        await _commandUserRepository.AddAsync((User)message);
                    }
                }
            };
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
                queueNameBuilder.Append(rabbitFieldsModel.QueueName);



                if (startTask.Keys.Contains(routingKey))
                {
                    var modelType = rabbitFieldsModel.ReceiverModelType;

                    await startTask[routingKey](message);
                }
            };

            _channel.BasicConsume(queue: queueNameBuilder.ToString(),
                                  autoAck: true,
                                  consumer: consumer);
        }

        private void InitializeRabbitMqConfiguration()
        {
            AddDirectExchangeWithQueueBinding("userQueue", "userExchange", "user.add");
            //AddDirectExchangeWithQueueBinding("userTest", "userExchange", "user.test");
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
