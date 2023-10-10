namespace Cnab.Api.Domain.Jwt
{
    public interface IJwtHandler
    {
        string GetClaims(string key);
    }
}
