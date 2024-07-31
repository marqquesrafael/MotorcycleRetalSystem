using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories.Motorcycle;
using MotorcycleRentalSystem.Domain.Interfaces.Services.Motorcycle;

namespace MotorcycleRentalSystem.Application.Services.Motorcycle
{
    public class CurrentYearMotorcycleService : BaseService<CurrentYearMotorcycleEntity>, ICurrentYearMotorcycleService
    {
        private readonly ICurrentYearMotorcycleRepository _repository;

        public CurrentYearMotorcycleService(ICurrentYearMotorcycleRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
