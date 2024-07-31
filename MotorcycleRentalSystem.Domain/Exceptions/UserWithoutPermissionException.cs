namespace MotorcycleRentalSystem.Domain.Exceptions
{
    public class UserWithoutPermissionException : Exception
    {
        public UserWithoutPermissionException() : base("Usuário não possui permissão a realizar esta operação") { }
    }
}
