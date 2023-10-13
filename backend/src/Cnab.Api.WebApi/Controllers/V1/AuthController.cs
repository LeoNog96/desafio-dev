using Cnab.Api.Contracts.Commands.Auth.Authenticate;
using Cnab.Api.Domain.NotificationPattern;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cnab.Api.WebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;
        private readonly INotificationContext _notificationContext;

        public AuthController(
            IMediator mediator,
            ILogger<AuthController> logger,
            INotificationContext notificationContext)
        {
            _mediator = mediator;
            _logger = logger;
            _notificationContext = notificationContext;
        }



        /// <summary>
        /// Método responsável por realizar o login de um usuário.
        /// </summary>
        /// <param name="request">Os dados de login do usuário.</param>
        /// <returns>Um IActionResult representando o resultado da operação.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AuthenticateCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(
            [FromBody] AuthenticateCommandRequest request,
            CancellationToken cancellation)
        {
            try
            {
                var response = await _mediator.Send(request, cancellation);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notificationContext.SetStatusCode(500);
                _notificationContext.AddNotification("Erro Servidor", "Login Falhou");
                return Problem();
            }
        }
    }
}
