using Cnab.Api.Domain.Entities;
using Cnab.Api.Domain.Enums;

namespace Cnab.Api.Test.Mock
{
    public static class TransactionMock
    {
        public static Transaction CreateFakeTransaction(Guid? userId = null)
        {
            if (userId == null)
            {
                userId = Guid.NewGuid();
            }

            return new Transaction
            {
                Type = ETransactionType.CREDIT, // Use algum valor válido para o enum ETransactionType
                Date = DateTime.UtcNow.AddDays(-1),
                Value = 100.50,
                Cpf = "12345678901",
                Card = "1234567890123456",
                UploadedBy = userId.Value,
                StoreOwner = "John Doe",
                StoreName = "John's Store"
            };
        }

    }
}
