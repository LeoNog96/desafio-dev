using Cnab.Api.Domain.Entities;

namespace Cnab.Api.Domain.Dtos
{
    public class ListTransactionPerStoreDto
    {
        public string StoreName { get; set; }
        public string StoreOwner { get; set; }
        public double Balance { get => Transaction.Sum(X => X.Value); }

        public IEnumerable<ListTransactionDto> Transaction { get; set; }
    }
}
