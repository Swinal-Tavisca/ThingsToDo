using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    public class GetSearchData : IGetSearch
    {
        private readonly IHttpClientFactory _httpClientFactory;
        IConfiguration _iconfiguration;

        public GetSearchData(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _iconfiguration = configuration;
        }
        public async Task<List<PlaceAttributes>> GetAllData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest,string AreaStatus)
        {
            try
            {
                var _googleClient = _httpClientFactory.CreateClient("GoogleClient");
                Uri endpoint = _googleClient.BaseAddress; // Returns GoogleApi
                var Key = _iconfiguration["GoogleAPI"];
                var Url = "";
                if(AreaStatus== "insideAirport")
                {
                     Url = endpoint.ToString() + "maps/api/place/textsearch/json?query=" + PointOfInterest + "+inside+" + DeparturePlace + "&language=en&key=" + Key;
                }
                else if(AreaStatus== "outsideAirport")
                {
                    String place = DeparturePlace;
                    place = place.Replace("Airport", "");
                    Url = endpoint.ToString() + "maps/api/place/textsearch/json?query=" + PointOfInterest + "+in+" + place + "&language=en&key=" + Key;
                }
                else
                {
                    Url = endpoint.ToString() + "maps/api/place/textsearch/json?query=" + PointOfInterest + "+in+" + DeparturePlace + "&language=en&key=" + Key;
                }

                

                var _client = _httpClientFactory.CreateClient();
                var response = await _client.GetAsync(Url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                RootobjectOfData data = JsonConvert.DeserializeObject<RootobjectOfData>(responseBody);
                List<PlaceAttributes> Data = data.results.TransalateData(Key, endpoint);

                //List<PlaceAttributes> SortedList = Data.OrderBy(o => o.Rating).ToList();

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

