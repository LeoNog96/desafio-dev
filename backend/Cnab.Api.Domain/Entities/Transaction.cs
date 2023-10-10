using Cnab.Api.Domain.Enums;

namespace Cnab.Api.Domain.Entities
{
    public class Transaction : Entity
    {
        public ETypeTransaction Type { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string Cpf { get; set; }
        public string Card { get; set; }
        public Guid UploadedBy { get; set; }
        public string StoreOwner { get; set; }
        public string StoreName { get; set; }
        public User User { get; set; }

        public TransactionTypes TransactionType { get; set; }
    }
}
