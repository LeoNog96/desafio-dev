using Cnab.Api.Domain.Entities;

namespace Cnab.Api.Test.Mock
{
    public static class TransactionTypesMock
    {
        public static TransactionTypes Generate()
            => new TransactionTypes
            {
                Description = "Débito",
                Id = Domain.Enums.ETransactionType.DEBIT,
                Kind = "Entrada",
                Signal = '+'
            };

        public static List<TransactionTypes> GenerateList()
        {
            return new List<TransactionTypes>()
            {
                new TransactionTypes
                {
                    Description = "Débito",
                    Id = Domain.Enums.ETransactionType.DEBIT,
                    Kind = "Entrada",
                    Signal = '+'
                },
                new TransactionTypes
                {
                    Description = "Boleto",
                    Id = Domain.Enums.ETransactionType.TICKET,
                    Kind = "Saída",
                    Signal = '-'
                },
                new TransactionTypes
                {
                    Description = "Financiamento",
                    Id = Domain.Enums.ETransactionType.FINANCING,
                    Kind = "Saída",
                    Signal = '-'
                },
                new TransactionTypes
                {
                    Description = "Crédito",
                    Id = Domain.Enums.ETransactionType.CREDIT,
                    Kind = "Entrada",
                    Signal = '+'
                },
                new TransactionTypes
                {
                    Description = "Recebimento Empréstimo",
                    Id = Domain.Enums.ETransactionType.LOAN_RECEIPT,
                    Kind = "Entrada",
                    Signal = '+'
                },
                new TransactionTypes
                {
                    Description = "Vendas",
                    Id = Domain.Enums.ETransactionType.SALES,
                    Kind = "Entrada",
                    Signal = '+'
                },
                new TransactionTypes
                {
                    Description = "Recebimento TED",
                    Id = Domain.Enums.ETransactionType.TED_RECEIPT,
                    Kind = "Entrada",
                    Signal = '+'
                },
                new TransactionTypes
                {
                    Description = "Recebimento DOC",
                    Id = Domain.Enums.ETransactionType.DOC_RECEIPT,
                    Kind = "Entrada",
                    Signal = '+'
                },
                new TransactionTypes
                {
                    Description = "Aluguel",
                    Id = Domain.Enums.ETransactionType.RENT,
                    Kind = "Saída",
                    Signal = '-'
                },
            };
        }
    }
}
