namespace MotorcycleRentalSystem.Domain.Configurations
{
    public class RabbitConfiguration
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }
}
