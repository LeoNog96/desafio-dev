using Cnab.Api.Contracts.Queries.Transactions;
using Cnab.Api.Domain.NotificationPattern;
using Cnab.Api.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cnab.Api.Application.Queries.V1
{
    public class ListTransactionQueryHandler : IRequestHandler<ListTransactionQueryRequest, ListTransactionQueryResponse>
    {
        private readonly ILogger<ListTransactionQueryHandler> _logger;
        private readonly INotificationContext _notificationContext;
        private readonly ITransactionRepository _transactionRepository;

        public ListTransactionQueryHandler(
            ILogger<ListTransactionQueryHandler> logger,
            INotificationContext notificationContext,
            ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _notificationContext = notificationContext;
            _transactionRepository = transactionRepository;
        }

        public async Task<ListTransactionQueryResponse> Handle(ListTransactionQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _transactionRepository.ListPaginateAsync(request, cancellationToken);

                return new ListTransactionQueryResponse(
                    response.Data, response.Total, response.PageNumber, response.PageSize
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notificationContext.AddNotification("Falha ao listar Transações", "Erro inesperado");
                return null;
            }
        }
    }
}
