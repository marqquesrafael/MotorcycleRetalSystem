using MotorcycleRentalSystem.Domain.Entities.Rider;
using MotorcycleRentalSystem.Domain.Requests;

namespace MotorcycleRentalSystem.Domain.Interfaces.Services.Rider
{
    public interface IRiderService : IBaseService<RiderEntity>
    {
        Task Register(RegisterRiderRequest request);
    }
}
