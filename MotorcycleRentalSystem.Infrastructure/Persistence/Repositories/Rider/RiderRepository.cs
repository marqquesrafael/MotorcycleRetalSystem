using MotorcycleRentalSystem.Domain.Entities.Rider;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Rider;
using MotorcycleRentalSystem.Infrastructure.Configuration;

namespace MotorcycleRentalSystem.Infrastructure.Persistence.Repositories.Rider
{
    public class RiderRepository : BaseRepository<RiderEntity>, IRiderRepository
    {
        public RiderRepository(MotorcycleRentalSystemDbContext dbContext) : base(dbContext)
        {
        }

        public bool IsCnpjAlreadyRegistered(string cnpj)
        {
            return Select().Any(p => p.Cnpj == cnpj);
        }

        public bool IsDriverLicenseAlreadyRegistered(string driverLicenseNumber)
        {
            return Select().Any(p => p.DriverLicenseNumber == driverLicenseNumber);
        }
    }
}
