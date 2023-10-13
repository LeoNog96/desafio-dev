using Cnab.Api.Contracts.Queries.Transactions.ListPerStore;
using Cnab.Api.Domain.NotificationPattern;
using Cnab.Api.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cnab.Api.Application.Queries.V1
{
    public sealed class ListPerStoreQueryHandler : IRequestHandler<ListPerStoreQueryRequest, ListPerStoreQueryResponse>
    {
        private readonly ILogger<ListTransactionQueryHandler> _logger;
        private readonly INotificationContext _notificationContext;
        private readonly ITransactionRepository _transactionRepository;

        public ListPerStoreQueryHandler(
            ILogger<ListTransactionQueryHandler> logger,
            INotificationContext notificationContext,
            ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _notificationContext = notificationContext;
            _transactionRepository = transactionRepository;
        }

        public async Task<ListPerStoreQueryResponse> Handle(
            ListPerStoreQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _transactionRepository.ListTransactionPerStore(cancellationToken);

                return new ListPerStoreQueryResponse(
                    response
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
