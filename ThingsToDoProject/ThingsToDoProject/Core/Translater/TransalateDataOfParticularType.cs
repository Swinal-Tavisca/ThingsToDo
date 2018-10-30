using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsToDoProject.Core.Interface;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core.Translater
{
    public static class TransalateDataOfParticularType
    {
        public static List<PlaceAttributes> TransalateData(this Result[] results, string Key , Uri Url)
        {
            List<PlaceAttributes> DataDetails = new List<PlaceAttributes>();

            for (int Index = 0; Index < results.Length; Index++)
            {
                //DataDetails.Add(new PlaceAttributes
                //{
                PlaceAttributes data = new PlaceAttributes();
                data.Name = results[Index].name ?? results[Index].name;
                data.Address = results[Index].formatted_address ?? results[Index].formatted_address;
                data.OpenClosedStatus = results[Index].opening_hours == null ? false : results[Index].opening_hours.open_now;
                data.Image = results[Index].photos == null ? null : Url + "maps/api/place/photo?maxwidth=400&photoreference=" + results[Index].photos[0].photo_reference + "&key=" + Key;
                data.PlaceID = results[Index].place_id == null ? null : results[Index].place_id;
                data.Rating = results[Index].rating == 0 ? 0 : results[Index].rating;
                data.Vicinity = results[Index].vicinity == null ? null : results[Index].vicinity;
                data.Latitude = results[Index].geometry.location.lat == 0 ? 0 : results[Index].geometry.location.lat;
                data.Longitude = results[Index].geometry.location.lng == 0 ? 0 : results[Index].geometry.location.lng;
                   // WeekDaysDetail = results[Index].opening_hours.weekday_text,//results.reviews == null ? null : results.reviews.ToList(),
                   // GoogleMapUrl = results[Index].url,
                    //Website = results[Index].website,

                //});
                DataDetails.Add(data);
            }
            return DataDetails;
        }
    }
}
