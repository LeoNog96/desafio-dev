using Cnab.Api.Domain.Dtos;
using Cnab.Api.Domain.Entities;
using System.Linq.Expressions;

namespace Cnab.Api.Domain.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        /// <summary>
        /// Busca um transação a partir de um id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Transaction> GetTransactionByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Lista as transaçoes carregadas pelo cnab de forma paginada
        /// </summary>
        /// <param name="pageNumber">número da página</param>
        /// <param name="pageSize">numero de itens por página</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<PaginateBaseDto<Transaction>> ListPaginateAsync(
            PaginateTransactionFilterDto paginateTransactionFilterDto,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Persistir um lote transações no banco
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<Transaction>> CreateRangeAsync(
            IEnumerable<Transaction> transaction,
            CancellationToken cancellationToken = default);
    }
}
