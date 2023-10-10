using Cnab.Api.Data.Context;
using Cnab.Api.Domain.Cache;
using Cnab.Api.Domain.Dtos;
using Cnab.Api.Domain.Entities;
using Cnab.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cnab.Api.Data.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(
            CnabContext db,
            ILogger<Transaction> logger,
            ICacheService cacheService)
            : base(db, logger, cacheService)
        {
        }

        public async Task<IEnumerable<Transaction>> CreateRangeAsync(
            IEnumerable<Transaction> transaction,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _db.AddRange(transaction);
                await _db.SaveChangesAsync(cancellationToken);
                await _cacheService.DeleteKeysByPrefixAsync<PaginateBaseDto<Transaction>>();
                return transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var transaction = await _db.Transactions.FindAsync(id, cancellationToken);
                _db.Entry(transaction).State = EntityState.Detached;
                return transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<PaginateBaseDto<Transaction>> ListPaginateAsync(
            PaginateTransactionFilterDto filterDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var key = $"{filterDto.PageNumber}_{filterDto.PageSize}{filterDto.StartDate}{filterDto.EndDate}{filterDto.Type}";
                var transactionsPaginate = await _cacheService
                    .GetCacheAsync<PaginateBaseDto<Transaction>>(key);

                if (transactionsPaginate == null)
                {

                    var query = _db.Transactions
                        .Where(transaction =>
                            (!filterDto.Type.HasValue || transaction.Type == filterDto.Type) &&
                            (!filterDto.StartDate.HasValue || transaction.Date >= filterDto.StartDate) &&
                            (!filterDto.EndDate.HasValue || transaction.Date >= filterDto.EndDate)
                        )
                        .OrderByDescending(x => x.CreatedAt);
                    var total = query.Count();

                    var listTake = query
                        .Skip(filterDto.PageSize * (filterDto.PageNumber - 1))
                        .Take(filterDto.PageSize);

                    transactionsPaginate = new PaginateBaseDto<Transaction>(
                        listTake,
                        total,
                        filterDto.PageNumber,
                        filterDto.PageSize);

                    await _cacheService
                        .SetCacheAsync(
                            transactionsPaginate,
                            TimeSpan.FromDays(15),
                            key
                            );
                }

                return transactionsPaginate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
