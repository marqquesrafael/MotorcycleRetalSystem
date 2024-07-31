using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Motorcycle;
using MotorcycleRentalSystem.Infrastructure.Configuration;

namespace MotorcycleRentalSystem.Infrastructure.Persistence.Repositories.Motorcyle
{
    public class CurrentYearMotorcycleRepository : BaseRepository<CurrentYearMotorcycleEntity>, ICurrentYearMotorcycleRepository
    {
        public CurrentYearMotorcycleRepository(MotorcycleRentalSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
