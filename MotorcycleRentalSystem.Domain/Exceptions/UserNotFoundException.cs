namespace MotorcycleRentalSystem.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userEmail) : base($"Usuário não encontrado para o email {userEmail}") { }
    }
}
