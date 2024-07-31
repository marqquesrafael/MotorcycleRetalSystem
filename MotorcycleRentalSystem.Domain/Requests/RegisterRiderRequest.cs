using Microsoft.AspNetCore.Http;

namespace MotorcycleRentalSystem.Domain.Requests
{
    public class RegisterRiderRequest
    {
        public string? Name { get; set; }
        public string? Cnpj { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? DriverLicenseNumber { get; set; }
        public string? DriverLicenseType { get; set; }
        public IFormFile? DriverLicenseImage { get; set; }
    }
}
