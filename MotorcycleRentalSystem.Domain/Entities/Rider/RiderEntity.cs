using MotorcycleRentalSystem.Domain.Enums;

namespace MotorcycleRentalSystem.Domain.Entities.Rider
{
    public class RiderEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriverLicenseNumber { get; set; }
        public DriverLicenseTypeEnum DriverLicenseType { get; set; }
        public string DriverLicenseImageKey { get; set; }
    }
}
