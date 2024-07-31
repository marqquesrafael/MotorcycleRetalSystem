using MotorcycleRentalSystem.Domain.Requests;

namespace MotorcycleRentalSystem.Domain.Interfaces.Services.RabbitMq
{
    public interface IRabbitMqClient
    {
        void Publish(MotorcycleRegisteredMessageEvent message);
    }
}
