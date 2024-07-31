using MotorcycleRentalSystem.Domain.Entities.User;

namespace MotorcycleRentalSystem.Domain.Interfaces.Repositories.User
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        UserEntity GetUserByEmailAndPassword(string email, string password);
    }
}
