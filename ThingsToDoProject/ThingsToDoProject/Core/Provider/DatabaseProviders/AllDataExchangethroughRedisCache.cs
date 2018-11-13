using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsToDoProject.Core.Interface.DatabaseContracts;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core.Provider.DatabaseProviders
{
    public class AllDataExchangethroughRedisCache : IAllDataExchangethroughRedisCache
    {
        static ConfigurationOptions option = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            EndPoints = { "localhost" }
        };
        public ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(option);
        public T GetDataFromCache<T>(string Key)
        {
            T data;
            try
            {
                IDatabase db = redis.GetDatabase();
                string val = db.StringGet(Key);
                if (val == null)
                {
                    return default(T);
                }
                data = JsonConvert.DeserializeObject<T>(val);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                data = default(T);
            }
            return data;
        }

        public void SaveInCache<T>(ref T PlaceData, string Key)
        {
            try
            {
                IDatabase db = redis.GetDatabase();
                string data = JsonConvert.SerializeObject(PlaceData);
                db.StringSet(Key, data, TimeSpan.FromHours(1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
