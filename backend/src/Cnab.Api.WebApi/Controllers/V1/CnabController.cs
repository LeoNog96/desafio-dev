using Cnab.Api.Contracts.Commands.Transactions.UploadTransaction;
using Cnab.Api.Domain.NotificationPattern;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cnab.Api.WebApi.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CnabController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CnabController> _logger;
        private readonly INotificationContext _notificationContext;

        public CnabController(IMediator mediator, ILogger<CnabController> logger, INotificationContext notificationContext)
        {
            _mediator = mediator;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        /// <summary>
        /// Método responsável por realizar o upload de um arquivo cnab.
        /// </summary>
        /// <param name="request">Arquivo cnab via form</param>
        /// <returns>Um IActionResult representando o resultado da operação.</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromForm] UploadTransactionCommandRequest request)
        {
            try
            {
                await _mediator.Send(request);

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notificationContext.SetStatusCode(500);
                _notificationContext.AddNotification("Erro Servidor", "upload Falhou");
                return Problem();
            }
        }
    }
}
