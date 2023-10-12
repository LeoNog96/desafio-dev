using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cnab.Api.Domain.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gera hash de senha com salto de complexidade 11
        /// </summary>
        /// <param name="password"> senha a ser encriptada </param>
        /// <returns> Retorna o hash da senha encriptada</returns>
        public static string PasswordCrypt(this string password) => BCrypt.Net.BCrypt.HashPassword(password);

        /// <summary>
        /// Compara um texto sem criptografia com o hash criptografado
        /// </summary>
        /// <param name="password"> senha em texto normal </param>
        /// <param name="hash"> hash da senha encriptada </param>
        /// <returns> Retorna True caso as senhas se coincida</returns>
        public static bool ComparePassword(this string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);

    }
}
