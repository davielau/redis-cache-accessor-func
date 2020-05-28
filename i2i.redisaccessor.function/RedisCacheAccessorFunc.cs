using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using i2i.redisaccessor.core.interfaces;

namespace i2i.redisaccessor.function
{
    public class RedisCacheAccessorFunc
    {
        private readonly IRedisCacheAccessor _redisCacheAccessor;

        public RedisCacheAccessorFunc(IRedisCacheAccessor redisCacheAccessor)
        {
            _redisCacheAccessor = redisCacheAccessor;
        }

        /// <summary>
        /// PUT request will add to the redis cache.  GET request will retrieve from the redis cache.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GetPutRedisCache")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "put", Route = null)] HttpRequest req, ILogger log)
        {

            var reqHeaders = req.Headers;
            string cacheKey = reqHeaders["cacheKey"];
            string cacheValue = reqHeaders["cacheValue"];
            string cacheConnectionString = Environment.GetEnvironmentVariable("redisCacheConnectionString");

            try
            {                
                string retValue = _redisCacheAccessor.ReadWriteFromCache(cacheConnectionString, req.Method, cacheKey, cacheValue);
                return new OkObjectResult(retValue);                
            }
            catch(Exception e)
            {
                log.LogError(e.Message, e);
                throw;
            }         
        }
    }
}
