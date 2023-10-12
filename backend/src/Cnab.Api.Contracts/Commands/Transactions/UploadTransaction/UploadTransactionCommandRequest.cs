using MediatR;
using Microsoft.AspNetCore.Http;

namespace Cnab.Api.Contracts.Commands.Transactions.UploadTransaction
{
    public class UploadTransactionCommandRequest : IRequest
    {
        public IFormFile File { get; set; }
    }
}
