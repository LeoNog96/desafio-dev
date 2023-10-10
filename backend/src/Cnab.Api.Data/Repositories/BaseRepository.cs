using Cnab.Api.Data.Context;
using Cnab.Api.Domain.Cache;
using Cnab.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Cnab.Api.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        protected readonly CnabContext _db;
        protected readonly ILogger<T> _logger;
        protected readonly ICacheService _cacheService;
        private bool _disposed;

        public BaseRepository(
            CnabContext db,
            ILogger<T> logger,
            ICacheService cacheService)
        {
            _db = db;
            _logger = logger;
            _cacheService = cacheService;
        }

        public IDbContextTransaction Transaction()
        {
            return _db.Database.BeginTransaction();
        }


        #region Disposable
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _disposed = true;
                _db.Dispose();
            }
        }
        #endregion
    }
}
