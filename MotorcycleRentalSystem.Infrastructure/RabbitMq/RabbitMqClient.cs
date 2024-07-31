using Microsoft.Extensions.Options;
using MotorcycleRentalSystem.Domain.Configurations;
using MotorcycleRentalSystem.Domain.Interfaces.Services.RabbitMq;
using MotorcycleRentalSystem.Domain.Requests;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MotorcycleRentalSystem.Infrastructure.RabbitMq
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        private readonly IConnection _connection;
        private readonly IModel _chanel;

        public RabbitMqClient(IOptions<RabbitConfiguration> rabbitConfiguration)
        {
            _rabbitConfiguration = rabbitConfiguration.Value;

            var connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitConfiguration.Host,
                UserName = _rabbitConfiguration.User,
                Password = _rabbitConfiguration.Password
            };

            _connection = connectionFactory.CreateConnection();
            _chanel = _connection.CreateModel();
        }

        public void Publish(MotorcycleRegisteredMessageEvent message)
        {
            string json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _chanel.BasicPublish(exchange: string.Empty,
                                 routingKey: _rabbitConfiguration.QueueName,
                                 basicProperties: null,
                                 body: body);
        }

    }
}
