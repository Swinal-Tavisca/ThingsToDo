using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core.Interface.DatabaseContracts
{
    public interface IAllDataExchangethroughRedisCache
    {
        T GetDataFromCache<T>(string Key);
        void SaveInCache<T>(ref T data, string v);
    }
}
