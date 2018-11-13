using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ThingsToDoProject.Core.Interface;
using ThingsToDoProject.Core.Interface.DatabaseContracts;
using ThingsToDoProject.Core.Translater;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core.Provider
{
    [Log]
    [Exception]
    public class GetDataOfParticularPlace : IGetPlaceData
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IGetDistanceTime _getDistanceTime;
        private readonly IGetLatitudeLongitude _getLatitudeLongitude;
        private readonly IAllDataExchangethroughRedisCache _allDataExchangethroughRedisCache;
        IConfiguration _iconfiguration;

        public GetDataOfParticularPlace(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IGetDistanceTime getDistanceTime, IGetLatitudeLongitude getLatitudeLongitude, IAllDataExchangethroughRedisCache allDataExchangethroughRedisCache)
        {
            _httpClientFactory = httpClientFactory;
            _iconfiguration = configuration;
            _getDistanceTime = getDistanceTime;
            _getLatitudeLongitude = getLatitudeLongitude;
            _allDataExchangethroughRedisCache = allDataExchangethroughRedisCache;
        }
        public async Task<PlaceAttributes> GetPlaceData(string DeparturePlace, string PlaceId)
        {
            try
            {

                var client = _httpClientFactory.CreateClient("GoogleClient");
                Uri endpoint = client.BaseAddress; // Returns GoogleApi
                var Key = _iconfiguration["GoogleAPI"];
                var Url = endpoint.ToString() + "maps/api/place/details/json?placeid=" + PlaceId + "&key=" + Key;
                var client1 = _httpClientFactory.CreateClient();
                var response = await client1.GetAsync(Url);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Rootobject data = JsonConvert.DeserializeObject<Rootobject>(responseBody);
                PlaceAttributes Data = data.result.TransalatePlaceData(Key, endpoint);
                DistanceTimeAttributes Journey = await _getDistanceTime.GetDistanceTime(DeparturePlace, Data.Latitude, Data.Longitude);
                Data.Distance = Journey.Distance;
                Data.Duration = Journey.Duration;
                _allDataExchangethroughRedisCache.SaveInCache<PlaceAttributes>(ref Data, PlaceId);
                return Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
