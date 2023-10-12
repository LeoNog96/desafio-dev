using Cnab.Api.Data.Context;
using Cnab.Api.Domain.Cache;
using Cnab.Api.Domain.Entities;
using Cnab.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cnab.Api.Data.Repositories
{
    public class TransactionTypeRepository : BaseRepository<TransactionTypes>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(
            CnabContext db,
            ILogger<TransactionTypes> logger,
            ICacheService cacheService) : base(db, logger, cacheService)
        {
        }

        public async Task<IEnumerable<TransactionTypes>> ListAsync()
        {
            var transactionTypes = await _cacheService.GetEnumerableCacheAsync<TransactionTypes>();

            if (transactionTypes == null)
            {
                transactionTypes = await _db.TransactionTypes.ToListAsync();
                await _cacheService.SetEnumerableCacheAsync<TransactionTypes>(transactionTypes,
                    TimeSpan.FromDays(30));
            }

            return transactionTypes;
        }
    }
}
