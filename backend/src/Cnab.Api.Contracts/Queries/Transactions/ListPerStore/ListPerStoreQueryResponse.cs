using Cnab.Api.Domain.Dtos;
using System.Collections;

namespace Cnab.Api.Contracts.Queries.Transactions.ListPerStore
{
    public class ListPerStoreQueryResponse : IEnumerable<ListTransactionPerStoreDto>
    {
        private readonly IEnumerable<ListTransactionPerStoreDto> _list;

        public ListPerStoreQueryResponse(IEnumerable<ListTransactionPerStoreDto> list)
        {
            _list = list;
        }

        public IEnumerator<ListTransactionPerStoreDto> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}