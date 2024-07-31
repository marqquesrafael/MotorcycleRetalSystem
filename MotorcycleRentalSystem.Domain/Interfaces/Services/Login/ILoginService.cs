using MotorcycleRentalSystem.Domain.Reponses.Token;

namespace MotorcycleRentalSystem.Domain.Interfaces.Services.Login
{
    public interface ILoginService
    {
        TokenResponse Authenticate(string email, string password);
    }
}
