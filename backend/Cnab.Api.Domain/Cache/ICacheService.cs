namespace Cnab.Api.Domain.Cache
{
    public interface ICacheService
    {
        /// <summary>
        /// Buscar no cache expecificando uma chave
        /// </summary>
        /// <param name="referencia"></param>
        /// <returns></returns>
        Task<T> GetCacheAsync<T>(object referencia = null);

        /// <summary>
        /// Buscar no cache expecificando uma chave
        /// </summary>
        /// <param name="referencia"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetEnumerableCacheAsync<T>(object referencia = null);

        /// <summary>
        /// Seta o cache com uma chave especifica
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="expiraEm"></param>
        /// <param name="referencia"></param>
        /// <returns></returns>
        Task SetCacheAsync<T>(T entidade, TimeSpan expiraEm, object referencia = null);


        /// <summary>
        /// Seta o cache com uma chave especifica
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="expiraEm"></param>
        /// <param name="referencia"></param>
        /// <returns></returns>
        Task SetEnumerableCacheAsync<T>(IEnumerable<T> entidade, TimeSpan expiraEm, object referencia = null);

        /// <summary>
        ///  Remover Cache
        /// </summary>
        /// <returns></returns>
        Task DeleteCacheAsync<T>(object referencia = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="referencia"></param>
        /// <returns></returns>
        Task DeleteEnumerableCacheAsync<T>(object referencia = null);

        /// <summary>
        /// Apaga Tudo que o prefixo for relacionado a T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task DeleteKeysByPrefixAsync<T>();
    }
}
