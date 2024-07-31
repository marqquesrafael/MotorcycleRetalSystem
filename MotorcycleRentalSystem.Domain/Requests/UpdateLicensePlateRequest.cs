namespace MotorcycleRentalSystem.Domain.Requests
{
    public class UpdateLicensePlateRequest
    {
        public long Id { get; set; }
        public string NewLicensePlate { get; set; }
    }
}
