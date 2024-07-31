namespace MotorcycleRentalSystem.Domain.Exceptions
{
    public class MotorcycleNotFoundException : Exception
    {
        public MotorcycleNotFoundException(long id) : base($"Moto não encontrada para o identificacdor {id} informado.")
        {

        }
    }
}
