using Cnab.Api.Domain.Dtos;
using MediatR;

namespace Cnab.Api.Contracts.Queries.Transactions.List
{
    public class ListTransactionQueryRequest : PaginateTransactionFilterDto, IRequest<ListTransactionQueryResponse>
    {
    }
}
