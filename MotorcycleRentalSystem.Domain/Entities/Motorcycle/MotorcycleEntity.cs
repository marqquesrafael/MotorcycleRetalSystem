namespace MotorcycleRentalSystem.Domain.Entities.Motorcycle
{
    public class MotorcycleEntity : BaseEntity
    {
        public long Year { get; set; }

        public string Model { get; set; }

        public string LicensePlate { get; set; }
    }
}
