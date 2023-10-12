namespace Cnab.Api.Domain.Dtos
{
    public class PaginateBaseDto<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }

        public int Total { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int PageTotal { get; set; }

        public PaginateBaseDto(IEnumerable<T> data, int total, int pageNumber, int pageSize)
        {
            Data = data;
            Total = total;
            PageNumber = pageNumber;
            PageSize = pageSize;
            PageTotal = (pageSize.Equals(0) ? 1 : ((total - 1) / pageSize + 1));
        }
    }
}
