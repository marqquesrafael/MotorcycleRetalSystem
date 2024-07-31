using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Domain.Reponses.Motorcycle;
using MotorcycleRentalSystem.Domain.Requests;

namespace MotorcycleRentalSystem.Domain.Interfaces.Services.Motorcycle
{
    public interface IMotorcycleService : IBaseService<MotorcycleEntity>
    {
        Task Register(AddMotorcycleRequest request, string userEmail);

        IEnumerable<TResponse> GetAll<TResponse>();

        MotorcycleResponse GetByLicensePlate(string licensePlate);

        Task UpdateLicensePlate(UpdateLicensePlateRequest request);
    }
}
