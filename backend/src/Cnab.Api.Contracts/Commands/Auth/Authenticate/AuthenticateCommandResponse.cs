namespace Cnab.Api.Contracts.Commands.Auth.Authenticate
{
    public class AuthenticateCommandResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}