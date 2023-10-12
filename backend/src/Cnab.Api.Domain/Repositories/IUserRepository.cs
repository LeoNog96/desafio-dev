using Cnab.Api.Domain.Entities;

namespace Cnab.Api.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Busca usuários por login
        /// </summary>
        /// <param name="login"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<User> GetbyLoginAsync(string login, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lista os usuários
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<User>> ListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Cria o usuário
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task CreateAsync(User user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Atualiza o usuário
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task UpdateAsync(User user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove o usuário logicamente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Removed(Guid id, CancellationToken cancellationToken = default);
    }
}
