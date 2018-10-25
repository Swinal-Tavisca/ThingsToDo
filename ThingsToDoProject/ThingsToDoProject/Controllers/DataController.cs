﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThingsToDoProject.Core.Interface;
using ThingsToDoProject.Core.Provider;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IGetOutsideData _getAllData;
        private readonly IGetData _getData;
        private readonly IGetLatitudeLongitude _getLatitudeLongitude;
        private readonly IGetInsideOutside _getInsideOutsideData;
        private readonly IGetPlaceData _getPlaceData;
        private readonly IGetDistanceTime _getDistanceTime;
        private readonly ISetReminder _setReminderData;
        public DataController(IGetOutsideData getAllData, IGetData getData, IGetLatitudeLongitude getLatitudeLongitude, IGetInsideOutside getInsideOutsideData, IGetPlaceData getPlaceData, IGetDistanceTime getDistanceTime,ISetReminder setReminderData)
        {
            _getAllData = getAllData;
            _getData = getData;
            _getLatitudeLongitude = getLatitudeLongitude;
            _getInsideOutsideData = getInsideOutsideData;
            _getPlaceData = getPlaceData;
            _getDistanceTime = getDistanceTime;
            _setReminderData = setReminderData;
        }

        //GET: api/Data/outsideAirport
        [HttpGet("outsideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}")]
        public async Task<IActionResult> GetOutsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest)
        {
            var Data = await _getAllData.GetAllData(DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Data Not Found");
        }

        //GET: api/Data/insideAirport
        [HttpGet("insideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}")]

        //PointOfInterest is any Stores/Restorents...etc
        public async Task<IActionResult> GetInsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest)
        {

            LocationAttributes Position = _getLatitudeLongitude.Get(DeparturePlace + "Airport");
            var Data = await _getData.GetData(Position, DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Not Found");
        }

        //GET: api/Data/InsideOutsideAirport
        [HttpGet("InsideOutsideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}")]
        public async Task<IActionResult> GetInsideOutsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest)
        {
            LocationAttributes Position = _getLatitudeLongitude.Get(DeparturePlace + "Airport");
            var Data = await _getInsideOutsideData.GetInsideOutsideData(Position, DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Not Found");
        }

        
        //GET: api/Data/position
        [HttpGet("position/{Location}")]
        public LocationAttributes GetPosition(string Location)
        {
            LocationAttributes Position = _getLatitudeLongitude.Get(Location);
            return Position;
        }

        //GET: api/Data/place
        [HttpGet("place/{DeparturePlace}/{PlaceId}")]
        public async Task<IActionResult> GetInfoOfParticularPlace(string DeparturePlace,string PlaceID)
        {
            var Data = await _getPlaceData.GetPlaceData(DeparturePlace,PlaceID);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Not Found");
        }
        [HttpPut ("reminder/{phoneNumber}/{placeId}/{name}/{distance}/{storeNumber}/{GoogleUrl}")]
        public void SetReminder(string phoneNumber,string placeId,string name,string distance,string storeNumber,string GoogleUrl)
        {
            
            _setReminderData.SetReminderForIternary(phoneNumber,placeId,name, distance,storeNumber,GoogleUrl);
        }

        ////GET: api/Data/distancetime
        //[HttpGet("distancetime/{DeparturePlace}/{DestinationPosition}")]
        //public async Task<IActionResult> GetDistanceTimeParticularPoints(String DeparturePlace,string DestinationPosition)
        //{
        //    LocationAttributes Position = _getLatitudeLongitude.Get(DeparturePlace + "Airport");
        //    var Data = await _getDistanceTime.GetDistanceTime(Position, DestinationPosition);
        //    if (Data != null)
        //        return Ok(Data);
        //    else
        //        return BadRequest("Not Found");
        //}

        ////GET: api/Data/placeAndDistanceTime
        //[HttpGet("placeAndDIstanceTime/{placeId}/{DeparturePlace}/{DestinationPosition}")]
        //public async Task<IActionResult> GetPlaceAndDistanceTime(String PlaceID, String DeparturePlace, string DestinationPosition)
        //{

        //}


        //// GET: api/Data
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Data/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Data
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Data/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
