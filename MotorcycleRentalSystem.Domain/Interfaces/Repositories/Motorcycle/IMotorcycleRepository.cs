using MotorcycleRentalSystem.Domain.Entities.Motorcycle;

namespace MotorcycleRentalSystem.Domain.Interfaces.Repositories.Motorcycle
{
    public interface IMotorcycleRepository : IBaseRepository<MotorcycleEntity>
    {
        bool HasMotorcycleByLicensePlate(string licensePlate);
    }
}
