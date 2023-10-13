using Cnab.Api.Data.Context;
using Cnab.Api.Domain.Cache;
using Cnab.Api.Domain.Entities;
using Cnab.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cnab.Api.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CnabContext db, ILogger<User> logger, ICacheService cache) 
            : base(db, logger, cache)
        {
        }

        public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                _db.Add(user);
                
                await _db.SaveChangesAsync(cancellationToken);
                await _cacheService.DeleteKeysByPrefixAsync<User>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<User> GetbyLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            try
            {               
                return await _db.Users
                        .Where(user => user.Login == login && !user.Removed)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
        }
    }
}
