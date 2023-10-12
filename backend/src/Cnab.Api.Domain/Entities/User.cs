namespace Cnab.Api.Domain.Entities
{
    public class User : Entity
    {
        public User()
            :base()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
