using i2i.redisaccessor.core.interfaces;
using StackExchange.Redis;
using System.Collections.Generic;

namespace i2i.redisaccessor.core
{
    public class CacheConnector: ICacheConnector
    {        
        /// <summary>
        /// Static dictionary to hold all redis connections.  Dictionary key is the 
        /// redis cache connection string
        /// </summary>
        private static Dictionary<string, ConnectionMultiplexer> redisConnections;
        
        public IDatabase getDbCache(string cacheConnectionString)
        { 
            //instantiate dictionary if null
            if (redisConnections == null)
            {
                redisConnections = new Dictionary<string, ConnectionMultiplexer>();                
            }

            //Retrieve the connection from the dictionary.  If it doesn't exist
            //add the connection to the dictionary
            ConnectionMultiplexer connection;
            if (!redisConnections.TryGetValue(cacheConnectionString, out connection))
            {
                connection = ConnectionMultiplexer.Connect(cacheConnectionString);
                redisConnections.Add(cacheConnectionString, connection);                
            }                        

            IDatabase dbCache = connection.GetDatabase();
            return dbCache;
        }
    }
}
