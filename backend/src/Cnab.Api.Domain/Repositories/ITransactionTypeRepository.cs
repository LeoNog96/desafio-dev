using Cnab.Api.Domain.Entities;

namespace Cnab.Api.Domain.Repositories
{
    public interface ITransactionTypeRepository : IBaseRepository<TransactionTypes>
    {
        Task<IEnumerable<TransactionTypes>> ListAsync();
    }
}
