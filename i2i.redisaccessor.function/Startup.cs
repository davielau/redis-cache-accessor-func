using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using i2i.redisaccessor.core.interfaces;
using i2i.redisaccessor.core;

[assembly: FunctionsStartup(typeof(i2i.redisaccessor.function.Startup))]
namespace i2i.redisaccessor.function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IRedisCacheAccessor, RedisCacheAccessor>();
            builder.Services.AddSingleton<ICacheConnector, CacheConnector>();            
        }
    }
}