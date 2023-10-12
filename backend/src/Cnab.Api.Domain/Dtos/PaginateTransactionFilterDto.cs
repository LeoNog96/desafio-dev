using Cnab.Api.Domain.Enums;

namespace Cnab.Api.Domain.Dtos
{
    public class PaginateTransactionFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public ETransactionType? Type { get; set; }
    }
}
