using Cnab.Api.Contracts.Commands.Auth.Authenticate;
using Cnab.Api.Domain.Extensions;
using Cnab.Api.Domain.Jwt;
using Cnab.Api.Domain.NotificationPattern;
using Cnab.Api.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cnab.Api.Application.Commands.V1
{
    public sealed class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommandRequest, AuthenticateCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly ILogger<AuthenticateCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;

        public AuthenticateCommandHandler(
            IUserRepository userRepository,
            TokenConfigurations tokenConfigurations,
            ILogger<AuthenticateCommandHandler> logger,
            INotificationContext notificationContext)
        {
            _userRepository = userRepository;
            _tokenConfigurations = tokenConfigurations;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task<AuthenticateCommandResponse> Handle(AuthenticateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetbyLoginAsync(request.Login, cancellationToken);

                if (user == null || !request.Password.ComparePassword(user?.Password))
                {
                    _notificationContext.AddNotification("Falhao ao realizar login", "Usuário ou senha inválidos");
                    return null;
                }
                var now = DateTime.Now;

                List<Claim> claims = new()
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", user.Id.ToString()),
                    new Claim("userName", user.Name.ToString())
                };

                var signingKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_tokenConfigurations.SecretKey)
                );
                var expires = now.AddDays(_tokenConfigurations.Days);

                var jwt = new JwtSecurityToken(
                    issuer: _tokenConfigurations.Emit,
                    audience: _tokenConfigurations.App,
                    claims: claims,
                    notBefore: now,
                    expires: expires,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return new AuthenticateCommandResponse
                {
                    Token = encodedJwt,
                    ExpiredAt = expires
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notificationContext.AddNotification("Falhao ao realizar login", "Erro inesperado");
                return null;
            }
        }
    }
}
