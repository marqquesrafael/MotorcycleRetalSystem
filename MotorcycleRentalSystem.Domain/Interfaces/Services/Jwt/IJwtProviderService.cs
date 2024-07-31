using MotorcycleRentalSystem.Domain.Entities.User;
using MotorcycleRentalSystem.Domain.Reponses.Token;

namespace MotorcycleRentalSystem.Domain.Interfaces.Services.Jwt
{
    public interface IJwtProviderService
    {
        TokenResponse Generate(UserEntity user);
    }
}
