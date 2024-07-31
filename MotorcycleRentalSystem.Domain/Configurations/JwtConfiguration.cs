namespace MotorcycleRentalSystem.Domain.Configurations
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }

        public int ExpirationInHour { get; set; }

        public string EmitedBy { get; set; }

        public string ValidatedIn { get; set; }
    }
}
