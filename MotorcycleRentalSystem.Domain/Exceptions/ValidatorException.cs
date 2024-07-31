namespace MotorcycleRentalSystem.Domain.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string error) : base(error) { }
    }
}
