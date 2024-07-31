using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Motorcycle;
using MotorcycleRentalSystem.Infrastructure.Configuration;

namespace MotorcycleRentalSystem.Infrastructure.Persistence.Repositories.Motorcyle
{
    public class MotorcycleRepository : BaseRepository<MotorcycleEntity>, IMotorcycleRepository
    {
        public MotorcycleRepository(MotorcycleRentalSystemDbContext dbContext) : base(dbContext)
        {
        }

        public bool HasMotorcycleByLicensePlate(string licensePlate) => Select().Any(p => p.LicensePlate == licensePlate && p.Active);

    }
}
