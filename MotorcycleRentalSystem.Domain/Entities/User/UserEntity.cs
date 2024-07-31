using MotorcycleRentalSystem.Domain.Enums.User;

namespace MotorcycleRentalSystem.Domain.Entities.User
{
    public class UserEntity : BaseEntity
    {
        public UserEntity(string email, string password, string fullName, string phoneNumber, UserTypeEnum type)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Type = type;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public UserTypeEnum Type { get; set; }

        public bool IsAdministrator() => Type == UserTypeEnum.Administrator ? true : false;
    }
}
