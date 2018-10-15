using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.DataAccess
{
    public class GetDataOfParticularType
    {
        public static List<TypeModel> GetAllDataOfParticularType(string type)
        {
            string URLString = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=28.556160,77.100281&radius=1000&type=" + type + "&key=AIzaSyB6-3S7muWP92RArV8jRd7p_DCeA24ecCU";
            WebClient webpage = new WebClient();
            string source = webpage.DownloadString(URLString);
            var data = (JObject)JsonConvert.DeserializeObject(source);
            var results = data["results"].Value<JArray>();

            List<TypeModel> StoreDetails = new List<TypeModel>();
            //var root = JsonConvert.DeserializeObject(results.ToString());
            //List<TypeModel> items = JsonConvert.DeserializeObject<List<TypeModel>>(results.ToString());
            for (var index = 0; index < results.Count; index++)
            {
                TypeModel store = new TypeModel();

                var res = (JObject)results[index];
                var placeidvalue = res["place_id"].Value<string>();
                store.PlaceID = placeidvalue.ToString();
                var name = res["name"].Value<string>();
                store.Name = name.ToString();
                var openingStatus = res["opening_hours"].Value<JObject>();
                var open = openingStatus["open_now"].Value<string>();
                store.OpenClosedStatus = open.ToString();
                StoreDetails.Add(store);
            }
            return StoreDetails;
        }
    }
}
