using Cnab.Api.Domain.Dtos;
using MediatR;

namespace Cnab.Api.Contracts.Queries.Transactions
{
    public class ListTransactionQueryRequest : PaginateTransactionFilterDto, IRequest<ListTransactionQueryResponse>
    {
    }
}
