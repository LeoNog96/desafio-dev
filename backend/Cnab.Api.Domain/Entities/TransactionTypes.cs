using Cnab.Api.Domain.Enums;

namespace Cnab.Api.Domain.Entities
{
    public class TransactionTypes
    {
        public TransactionTypes()
        {
            Transactions = new HashSet<Transaction>();
        }

        public ETypeTransaction Id { get; set; }
        public string Description { get; set; }
        public string Kind { get; set; }
        public char Signal { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
