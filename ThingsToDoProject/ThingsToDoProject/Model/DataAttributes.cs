using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThingsToDoProject.Model
{
    public class DataAttributes
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string OpenClosedStatus { get; set; }
        public string PhotoReference { get; set; }
        public string PlaceID { get; set; }
        public int Rating { get; set; }
        public string Vicinity { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
