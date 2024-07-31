using MotorcycleRentalSystem.Domain.Entities.Rider;

namespace MotorcycleRentalSystem.Domain.Interfaces.Repositories.Rider
{
    public interface IRiderRepository : IBaseRepository<RiderEntity>
    {
        bool IsCnpjAlreadyRegistered(string cnpj);

        bool IsDriverLicenseAlreadyRegistered(string driverLicenseNumber);
    }
}
