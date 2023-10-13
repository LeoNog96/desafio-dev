using Cnab.Api.Contracts.Queries.Transactions;
using Cnab.Api.Contracts.Queries.Transactions.List;
using Cnab.Api.Contracts.Queries.Transactions.ListPerStore;
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
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionsController> _logger;
        private readonly INotificationContext _notificationContext;

        public TransactionsController(
            IMediator mediator,
            ILogger<TransactionsController> logger,
            INotificationContext notificationContext)
        {
            _mediator = mediator;
            _logger = logger;
            _notificationContext = notificationContext;
        }



        /// <summary>
        /// Método responsável por realizar listagem das transacoes de forma paginada.
        /// </summary>
        /// <param name="request">Filtros de consulta</param>
        /// <returns>Um IActionResult representando o resultado da operação.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ListTransactionQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromQuery]
        ListTransactionQueryRequest request, CancellationToken cancellation)
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
                _notificationContext.AddNotification("Erro Servidor", "Listagem Falhou");
                return Problem();
            }
        }

        /// <summary>
        /// Método responsável por realizar listagem das transacoes agrupadas por loja.
        /// </summary>
        /// <param name="request">Filtros de consulta</param>
        /// <returns>Um IActionResult representando o resultado da operação.</returns>
        [HttpGet("per-store")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ListPerStoreQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPerStore(CancellationToken cancellation)
        {
            try
            {
                var response = await _mediator.Send(new ListPerStoreQueryRequest(), cancellation);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notificationContext.SetStatusCode(500);
                _notificationContext.AddNotification("Erro Servidor", "Listagem Falhou");
                return Problem();
            }
        }
    }
}
