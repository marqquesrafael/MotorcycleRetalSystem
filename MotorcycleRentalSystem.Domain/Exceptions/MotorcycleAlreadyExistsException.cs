namespace MotorcycleRentalSystem.Domain.Exceptions
{
    public class MotorcycleAlreadyExistsException : Exception
    {
        public MotorcycleAlreadyExistsException(string licensePlate) : base($"Não foi possível concluir o registro pois já existe uma moto cadastrada para a placa {licensePlate}")
        {

        }
    }
}
