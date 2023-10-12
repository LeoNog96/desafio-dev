using Cnab.Api.Domain.Dtos;

namespace Cnab.Api.Contracts.Queries.Transactions
{
    public class ListTransactionQueryResponse : PaginateBaseDto<ListTransactionDto>
    {
        public ListTransactionQueryResponse(
            IEnumerable<ListTransactionDto> data,
            int total, int pageNumber, int pageSize) 
            : base(data, total, pageNumber, pageSize)
        {
        }
    }
}