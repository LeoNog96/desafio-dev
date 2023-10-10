using Cnab.Api.Domain.Jwt;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Cnab.Api.Application.Jwt
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public JwtHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetClaims(string key)
        {
            return _accessor.HttpContext.User.FindFirstValue(key);
        }
    }
}
