using Cnab.Api.Domain.Entities;
using Cnab.Api.Domain.Enums;
using Cnab.Api.Domain.NotificationPattern;
using Cnab.Api.Domain.Repositories;
using Cnab.Api.Domain.Services;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

namespace Cnab.Api.Services
{
    public class CnabService : ICnabService
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger<CnabService> _logger;

        public CnabService(ITransactionTypeRepository transactionTypeRepository, INotificationContext notificationContext, ILogger<CnabService> logger)
        {
            _transactionTypeRepository = transactionTypeRepository;
            _notificationContext = notificationContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Transaction>> ProccessFileAsync(Stream stream, Guid userId)
        {
            var transactionsTypes = await _transactionTypeRepository.ListAsync();
            List<Transaction> transactions = new();
            try
            {
                using (StreamReader reader = new(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var linha = reader.ReadLine();

                        var date = DateTime.ParseExact(linha.Substring(1, 8), "yyyyMMdd", null);
                        date += TimeSpan.FromSeconds(int.Parse(linha.Substring(42, 6)));

                        var etype = (ETransactionType)Enum.Parse(typeof(ETransactionType), linha.Substring(0, 1));
                        var type = transactionsTypes.Where(x => x.Id == etype).FirstOrDefault();

                        var transaction = new Transaction
                        {
                            Type = etype,
                            Date = date,
                            UploadedBy = userId
                        };

                        transaction.Value = GetValue(linha.Substring(9, 10), type);
                        transaction.Cpf = linha.Substring(19, 11);
                        transaction.Card = linha.Substring(30, 12);
                        transaction.StoreOwner = linha.Substring(48, 14).Trim();
                        transaction.StoreName = linha.Substring(62, 18).Trim();

                        transactions.Add(transaction);
                    }
                }

                return transactions;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notificationContext.AddNotification("Falha ao processar o arquivo", "arquivo corrompido ou com formato diferente");
                return null;
            }
        }

        private static double GetValue(string valueString, TransactionTypes transactionType)
        {
            var value = double.Parse(valueString) / 100.00;

            if (transactionType.Signal == '-') return value *= -1;
            return value;
        }
    }
}
