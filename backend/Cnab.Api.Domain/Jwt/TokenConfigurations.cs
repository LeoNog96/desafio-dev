namespace Cnab.Api.Domain.Jwt
{
    public class TokenConfigurations
    {
        public string SecretKey { get; set; }
        public string Emit { get; set; }
        public string App { get; set; }
        public int Days { get; set; }
    }
}
