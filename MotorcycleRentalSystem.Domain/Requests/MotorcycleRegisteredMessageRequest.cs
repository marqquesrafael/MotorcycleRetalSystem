namespace MotorcycleRentalSystem.Domain.Requests
{
    public class MotorcycleRegisteredMessageEvent
    {
        public MotorcycleRegisteredMessageEvent(long id, long year, string model, string licensePlate, long createdBy)
        {
            Id = id;
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
            CreatedBy = createdBy;
        }

        public long Id { get; set; }
        public long Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public long CreatedBy { get; set; }
    }
}
