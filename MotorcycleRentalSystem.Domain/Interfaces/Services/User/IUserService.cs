using MotorcycleRentalSystem.Domain.Entities.User;

namespace MotorcycleRentalSystem.Domain.Interfaces.Services.User
{
    public interface IUserService : IBaseService<UserEntity>
    {
        UserEntity FindUserByEmailAndPassword(string email, string password);

        UserEntity FindUserByEmail(string email);
    }
}
