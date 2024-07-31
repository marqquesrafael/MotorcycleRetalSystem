namespace MotorcycleRentalSystem.Domain.Exceptions
{
    public class UpdateWithoutIdException : Exception
    {
        public UpdateWithoutIdException() : base($"Não é possivel atualizar o registro sem o identificador") { }
    }
}
