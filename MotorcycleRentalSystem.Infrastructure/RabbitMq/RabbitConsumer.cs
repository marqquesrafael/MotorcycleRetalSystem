using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MotorcycleRentalSystem.Domain.Configurations;
using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Motorcycle;
using MotorcycleRentalSystem.Domain.Requests;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MotorcycleRentalSystem.WebApi.RabbitConsumer
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly RabbitConfiguration _rabbitConfiguration;
        private readonly IModel _chanel;

        public RabbitMqConsumer(IOptions<RabbitConfiguration> rabbitConfiguration, IServiceScopeFactory serviceScopeFactory)
        {
            _rabbitConfiguration = rabbitConfiguration.Value;
            _serviceScopeFactory = serviceScopeFactory;

            _chanel = new ConnectionFactory()
            {
                HostName = _rabbitConfiguration.Host,
                UserName = _rabbitConfiguration.User,
                Password = _rabbitConfiguration.Password
            }
            .CreateConnection()
            .CreateModel();

            _chanel.QueueDeclare(_rabbitConfiguration.QueueName, true, false, false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Receive();
            return Task.CompletedTask;
        }

        public void Receive()
        {
            var consumer = new EventingBasicConsumer(_chanel);
            consumer.Received += (model, ea) =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var currentMotorcycleService = scope.ServiceProvider.GetService<ICurrentYearMotorcycleService>();

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var @event = JsonSerializer.Deserialize<MotorcycleRegisteredMessageEvent>(message);

                    Console.Write("Message MotorcycleRegisteredMessageEvent received");

                    if (@event.Year == DateTime.UtcNow.Year)
                        currentMotorcycleService.Create(new CurrentYearMotorcycleEntity(@event.CreatedBy, @event.Id));

                    _chanel.BasicAck(ea.DeliveryTag, false);
                }
            };

            _chanel.BasicConsume(queue: _rabbitConfiguration.QueueName,
                                 autoAck: false,
                                 consumer: consumer);
        }
    }
}
