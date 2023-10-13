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
                await _cacheService.DeleteKeysByPrefixAsync<ListTransactionPerStoreDto>();
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

        public async Task<PaginateBaseDto<ListTransactionDto>> ListPaginateAsync(
            PaginateTransactionFilterDto filterDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _db.Transactions
                        .Where(transaction =>
                            (!filterDto.Type.HasValue || transaction.Type == filterDto.Type) &&
                            (!filterDto.StartDate.HasValue || transaction.Date >= filterDto.StartDate) &&
                            (!filterDto.EndDate.HasValue || transaction.Date <= filterDto.EndDate)
                        )
                        .Include(x => x.User)
                        .Include(t => t.TransactionType)
                        .OrderByDescending(x => x.CreatedAt)
                        .Select(x => new ListTransactionDto
                        {
                            Card = x.Card,
                            Type = x.TransactionType.Description,
                            StoreName = x.StoreName,
                            Cpf = x.Cpf,
                            Date = x.Date,
                            StoreOwner = x.StoreOwner,
                            Value = x.Value,
                            UploadedBy = x.User.Name,
                        });
                var total = query.Count();

                var listTake = query
                    .Skip(filterDto.PageSize * (filterDto.PageNumber - 1))
                    .Take(filterDto.PageSize);

                var transactionsPaginate = new PaginateBaseDto<ListTransactionDto>(
                    listTake,
                    total,
                    filterDto.PageNumber,
                    filterDto.PageSize);

                return transactionsPaginate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<ListTransactionPerStoreDto>> ListTransactionPerStore(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var transactions = await _cacheService.GetEnumerableCacheAsync<ListTransactionPerStoreDto>();

                if (transactions == null)
                {
                    var rawGroupedData = await _db.Transactions
                        .Include(t => t.TransactionType)
                        .AsNoTracking()
                        .GroupBy(t => new { t.StoreName, t.StoreOwner })
                        .ToListAsync(cancellationToken);

                    transactions = rawGroupedData
                        .Select(group => new ListTransactionPerStoreDto
                        {
                            StoreName = group.Key.StoreName,
                            StoreOwner = group.Key.StoreOwner,
                            Transaction = group.AsEnumerable().Select(x => new ListTransactionDto
                            {
                                Card = x.Card,
                                Type = x.TransactionType.Description,
                                StoreName = x.StoreName,
                                Cpf = x.Cpf,
                                Date = x.Date,
                                StoreOwner = x.StoreOwner,
                                Value = x.Value
                            })
                        })
                        .ToList();

                    await _cacheService.SetEnumerableCacheAsync(transactions, TimeSpan.FromDays(15));
                }

                return transactions;                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
