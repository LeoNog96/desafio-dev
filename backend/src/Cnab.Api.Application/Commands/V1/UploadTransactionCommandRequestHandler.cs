using Cnab.Api.Contracts.Commands.Transactions.UploadTransaction;
using Cnab.Api.Domain.Jwt;
using Cnab.Api.Domain.NotificationPattern;
using Cnab.Api.Domain.Repositories;
using Cnab.Api.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cnab.Api.Application.Commands.V1
{
    public sealed class UploadTransactionCommandRequestHandler : IRequestHandler<UploadTransactionCommandRequest>
    {
        private readonly ILogger<UploadTransactionCommandRequestHandler> _logger;
        private readonly IJwtHandler _jwtHandler;
        private readonly ICnabService _cnabService;
        private readonly INotificationContext _notificationContext;
        private readonly ITransactionRepository _transactionRepository;

        public UploadTransactionCommandRequestHandler(
            ILogger<UploadTransactionCommandRequestHandler> logger,
            IJwtHandler jwtHandler,
            ICnabService cnabService,
            INotificationContext notificationContext,
            ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _jwtHandler = jwtHandler;
            _cnabService = cnabService;
            _notificationContext = notificationContext;
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(UploadTransactionCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = new Guid(_jwtHandler.GetClaims("userId"));
                var transactions = await _cnabService.ProccessFileAsync(request.File.OpenReadStream(), userId);

                if (transactions == null)
                {
                    return;
                }
                _ = await _transactionRepository.CreateRangeAsync(transactions, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notificationContext.AddNotification("Falha no uplaod de arquivo", "Erro inesperado");
            }
        }
    }
}
