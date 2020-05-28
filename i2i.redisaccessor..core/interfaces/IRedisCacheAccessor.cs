namespace i2i.redisaccessor.core.interfaces
{
    public interface IRedisCacheAccessor
    {
        /// <summary>
        /// read or write value from redis cache
        /// </summary>
        /// <param name="cacheConnectionString">redis cache connection string</param>
        /// <param name="reqMethod">PUT or GET</param>
        /// <param name="cacheKey">cache key</param>
        /// <param name="cacheValue">cache value</param>
        /// <returns>return string value from cache or blank if adding to cache</returns>
        string ReadWriteFromCache(string cacheConnectionString, string reqMethod, string cacheKey, string cacheValue);        
        
    }
}
