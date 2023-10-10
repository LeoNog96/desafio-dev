using Microsoft.EntityFrameworkCore.Storage;

namespace Cnab.Api.Domain.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        IDbContextTransaction Transaction();
    }
}
