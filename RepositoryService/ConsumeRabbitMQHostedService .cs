using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Domain.Models;
using System.Text.Json;
using RepositoryService.Command.Interfaces;
using RepositoryService.Background.Interfaces;

namespace RepositoryService
{
    public class ConsumeRabbitMQHostedService : BackgroundService
    {
        private readonly IRabbitMqConsumer _rabbitMqConnection;

        public ConsumeRabbitMQHostedService(IRabbitMqConsumer rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                stoppingToken.ThrowIfCancellationRequested();

                _rabbitMqConnection.ReceiveMessages();
            }
        }

        private void _connection_ConnectionShutdown(object sender,
                                                    ShutdownEventArgs e)
        {
            Console.WriteLine("Connection shutdown");
        }

        //public override void Dispose()
        //{
        //    if (_channel.IsOpen)
        //    {
        //        _channel.Close();
        //        _connection.Close();
        //    }
        //    base.Dispose();
        //}
    }
}
