namespace WebApi.Utility
{
    public class JwtTokenOptions
    {
        public string SecretKey { get; set; } = string.Empty;

        public int ExpiresMinutes { get; set; }
    }
}
