using Microsoft.Extensions.Logging;
using i2i.redisaccessor.core.interfaces;
using StackExchange.Redis;
using System;

namespace i2i.redisaccessor.core
{
    public class RedisCacheAccessor: IRedisCacheAccessor
    {       
        private readonly ICacheConnector _cacheConnector;
        private readonly ILogger log;

        public RedisCacheAccessor(ICacheConnector cacheConnector, ILoggerFactory loggerFactory)
        {
            _cacheConnector = cacheConnector;
            log = loggerFactory.CreateLogger("RedisCacheAccessor");
        }
       
        /// <summary>
        /// Read and write from the redis cache
        /// </summary>
        /// <param name="cacheConnectionString"></param>
        /// <param name="reqMethod"></param>
        /// <param name="cacheKey"></param>
        /// <param name="cacheValue"></param>
        /// <returns>Returns the cache value for GET requests and the cache key for PUT requests</returns>
        public string ReadWriteFromCache(string cacheConnectionString, string reqMethod, string cacheKey, string cacheValue)
        {
            IDatabase dbCache = _cacheConnector.getDbCache(cacheConnectionString);

            if (reqMethod == System.Net.WebRequestMethods.Http.Put)
            {
                dbCache.StringSet(cacheKey, cacheValue, new TimeSpan(24, 0, 0));
                log.LogInformation(String.Format("PUT cacheKey={0} cacheValue={1} ", cacheKey, cacheValue));
                return cacheKey;
            }
            else if (reqMethod == System.Net.WebRequestMethods.Http.Get)
            {
                string cacheReturn = (string)dbCache.StringGet(cacheKey) ?? "Unknown";
                log.LogInformation(String.Format("GET cacheKey={0} cacheValue={1} ", cacheKey, cacheReturn));
                return cacheReturn;
            }
            else
            {
                throw new Exception(reqMethod + "method not allowed");
            }                    
        }
    }
}
