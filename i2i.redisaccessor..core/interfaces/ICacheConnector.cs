using StackExchange.Redis;

namespace i2i.redisaccessor.core.interfaces
{
    public interface ICacheConnector
    {
        /// <summary>
        /// Return the redis cache database based on connection string
        /// </summary>
        /// <param name="cacheConnectionString">redis cache connection string</param>
        /// <returns></returns>
        IDatabase getDbCache(string cacheConnectionString);
    }
}
