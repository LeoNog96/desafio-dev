using Cnab.Api.Domain.Entities;
using Cnab.Api.Domain.Extensions;

namespace Cnab.Api.Test.Mock
{
    public static class UserMock
    {
        public static User Generate()
            => new()
            {
                Name = "Administrador",
                Login = "admin",
                Password = "102030".PasswordCrypt(),
            };
    }
}
