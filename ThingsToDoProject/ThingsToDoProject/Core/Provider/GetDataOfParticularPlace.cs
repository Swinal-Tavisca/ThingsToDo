using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ThingsToDoProject.Core.Interface;
using ThingsToDoProject.Core.Translater;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core.Provider
{
    public class GetDataOfParticularPlace : IGetPlaceData
    {
        private readonly IHttpClientFactory _httpClientFactory;
        IConfiguration _iconfiguration;

        public GetDataOfParticularPlace(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _iconfiguration = configuration;
        }
        public async Task<List<PlaceAttributes>> GetPlaceData(string PlaceId)
        {
            try
            {
                //Rootobject obj = new Rootobject();
                //using (HttpClient client = new HttpClient())
                //{
                var client = _httpClientFactory.CreateClient("GoogleClient");
                Uri endpoint = client.BaseAddress; // Returns GoogleApi
                var Key = _iconfiguration["GoogleAPI"];
                var Url = endpoint.ToString() + "maps/api/place/details/json?placeid=" + PlaceId + "&key=" + Key;
                var client1 = _httpClientFactory.CreateClient();
                var response = await client1.GetAsync(Url);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ////var finalObject = JsonConvert.DeserializeObject<>(responseBody);
                Rootobject data = JsonConvert.DeserializeObject<Rootobject>(responseBody);
                //var results = data["result"].Value<JArray>();
                //Result obj = new Result();
                //var address= obj1.adr_address;
                List<PlaceAttributes> Data = data.result.TransalatePlaceData(Key, endpoint);
                return Data;
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}
