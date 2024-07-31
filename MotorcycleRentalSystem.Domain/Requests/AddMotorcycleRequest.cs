namespace MotorcycleRentalSystem.Domain.Requests
{
    public class AddMotorcycleRequest
    {
        public long Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
    }
}
