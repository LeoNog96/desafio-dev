using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cnab.Api.Contracts.Commands.Auth.Authenticate
{
    public class AuthenticateCommandRequest : IRequest<AuthenticateCommandResponse>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
