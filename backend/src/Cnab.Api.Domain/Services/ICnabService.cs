
using Cnab.Api.Domain.Entities;

namespace Cnab.Api.Domain.Services
{
    public interface ICnabService
    {
        Task<IEnumerable<Transaction>> ProccessFileAsync(Stream stream, Guid userId);
    }
}
