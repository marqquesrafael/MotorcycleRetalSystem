namespace MotorcycleRentalSystem.Domain.Exceptions
{
    public class InvalidImageTypeException : Exception
    {
        public InvalidImageTypeException() : base("Formato de arquivo inválido. Apenas PNG e BMP são suportados.") {}
    }
}
