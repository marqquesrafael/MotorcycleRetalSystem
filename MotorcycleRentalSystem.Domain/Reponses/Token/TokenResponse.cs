namespace MotorcycleRentalSystem.Domain.Reponses.Token
{
    public class TokenResponse
    {
        public TokenResponse() => TokenType = "Bearer";

        public string Token { get; set; }

        public string TokenType { get; set; }

        public int ExpireInHour { get; set; }
    }
}
