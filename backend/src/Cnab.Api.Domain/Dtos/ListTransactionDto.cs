using Cnab.Api.Domain.Enums;

namespace Cnab.Api.Domain.Dtos
{
    public class ListTransactionDto
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string Cpf { get; set; }
        public string Card { get; set; }
        public string UploadedBy { get; set; }
        public string StoreOwner { get; set; }
        public string StoreName { get; set; }
    }
}
