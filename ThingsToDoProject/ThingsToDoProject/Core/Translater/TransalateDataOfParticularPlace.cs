using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core.Translater
{
    public static class TransalateDataOfParticularPlace
    {
        public static List<PlaceAttributes> TransalatePlaceData(this Result results, string Key, Uri Url)
        {
            //Opening_Hours obj = new Opening_Hours();
            List<PlaceAttributes> PlaceDetails = new List<PlaceAttributes>();
            List<Result> PlaceDetail = new List<Result>();
            //Result Place = new Result();
            PlaceDetails.Add(new PlaceAttributes//Result
            {
                Name = results.name,
                Address = results.formatted_address,
                OpenClosedStatus = results.opening_hours.open_now,
                Image = Url + "maps/api/place/photo?maxwidth=400&photoreference=" + results.photos[0].photo_reference + "&key=" + Key,
                PlaceID = results.place_id,
                Rating = results.rating,
                Vicinity = results.vicinity,
                Latitude = results.geometry.location.lat,
                Longitude = results.geometry.location.lng,
                PhoneNumber = results.formatted_phone_number,
                Reviews = GetReviews(results.reviews),
                WeekDaysDetail = results.opening_hours.weekday_text,//results.reviews == null ? null : results.reviews.ToList(),
                GoogleMapUrl = results.url,
                Website = results.website

            });
            PlaceDetail.Add(new Result
            {
               // reviews = results.reviews,
            });
            //for (var index = 0; index < sizeof(results); index++)
            //{

            //    //Place.

            //    var resultObject = (JObject)results[index];

            //    Place.Name = resultObject["name"].Value<string>();

            //    try { Place.Address = resultObject["formatted_address"].Value<string>(); }
            //    catch { Place.Address = "Address Not Available"; }

            //    try
            //    {
            //        var openingStatus = resultObject["opening_hours"].Value<JObject>();
            //        Place.OpenClosedStatus = openingStatus["open_now"].Value<string>();
            //    }
            //    catch { Place.OpenClosedStatus = "Status Not Available"; }

            //    try
            //    {
            //        var photos = resultObject["photos"].Value<JArray>();
            //        var photosObject = (JObject)photos[0];
            //        Place.Image = Url + "maps/api/place/photo?maxwidth=400&photoreference=" + photosObject["photo_reference"].Value<string>() + "&key =" + Key;
            //    }
            //    catch { Place.Image = "Image Not Available"; }

            //    try { Place.PlaceID = resultObject["place_id"].Value<string>(); }
            //    catch { Place.PlaceID = "Place ID is Not Available"; }

            //    try { Place.Rating = Convert.ToInt32(resultObject["rating"].Value<string>()); }
            //    catch { Place.Rating = -1; }

            //    try { Place.Vicinity = resultObject["vicinity"].Value<string>(); }
            //    catch { Place.Vicinity = "Not Available"; }
            //    var geometry = resultObject["geometry"].Value<JObject>();
            //    var location = geometry["location"].Value<JObject>();
            //    Place.Longitude = location["lng"].Value<string>();
            //    Place.Latitude = location["lat"].Value<string>();
            //    try { Place.PhoneNumber = resultObject["formatted_phone_number"].Value<string>(); }
            //    catch { Place.PhoneNumber = "Phone Number is Not Available"; }
            //    try { Place.WeekDaysDetail = resultObject["weekday_text"].Value<JArray>(); } catch { }
            //    try { Place.Reviews = resultObject["reviews"].Value<JArray>(); } catch { }
            //    try { Place.GoogleMapUrl = resultObject["url"].Value<string>(); }
            //    catch { Place.GoogleMapUrl = "Url is Not Available"; }
            //    PlaceDetails.Add(Place);
            //}
            return PlaceDetails;
        }

        private static List<AllReview> GetReviews(Review[] reviews)
        {
            int count = 0;
            return reviews.Select(x => new AllReview()
            {
                author_name = reviews[count].author_name,
                text = reviews[count].text,
                rating = reviews[count++].rating,
            }).ToList();
        }
    }
}
//v11 pro 20.5 funtouch
//    f9 "11.5 color 