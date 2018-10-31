using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThingsToDoProject.Core;
using ThingsToDoProject.Core.Interface;
using ThingsToDoProject.Core.Interface.DatabaseContracts;
using ThingsToDoProject.Core.Provider;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Log]
    [Exception]
    public class DataController : ControllerBase
    {
        private readonly IGetOutsideData _getAllData;
        private readonly IGetData _getData;
        private readonly IGetLatitudeLongitude _getLatitudeLongitude;
        private readonly IGetInsideOutside _getInsideOutsideData;
        private readonly IGetPlaceData _getPlaceData;
        private readonly IGetDistanceTime _getDistanceTime;
        private readonly ISetReminder _setReminderData;
        private readonly IGetSearch _getSearch;
        private readonly IGetDataAccordingToLayoverTime _getDataAccordingToLayoverTime;
        private readonly IAllDataExchangethroughRedisCache _allDataExchangethroughRedisCache;
        public DataController(IGetOutsideData getAllData, IGetData getData, IGetLatitudeLongitude getLatitudeLongitude, 
            IGetInsideOutside getInsideOutsideData, IGetPlaceData getPlaceData, IGetDistanceTime getDistanceTime, 
            ISetReminder setReminderData,IGetSearch getSearch, IGetDataAccordingToLayoverTime getDataAccordingToLayoverTime, IAllDataExchangethroughRedisCache allDataExchangethroughRedisCache)
        {
            _getAllData = getAllData;
            _getData = getData;
            _getLatitudeLongitude = getLatitudeLongitude;
            _getInsideOutsideData = getInsideOutsideData;
            _getPlaceData = getPlaceData;
            _getDistanceTime = getDistanceTime;
            _setReminderData = setReminderData;
            _getSearch = getSearch;
            _getDataAccordingToLayoverTime = getDataAccordingToLayoverTime;
            _allDataExchangethroughRedisCache = allDataExchangethroughRedisCache;
        }
        //GET: api/Data/search
        [HttpGet("search/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}/{LayoverTime}/{AreaStatus}")]
        public async Task<IActionResult> GetSearchData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest,int LayoverTime,string AreaStatus)
        {
            string FilterKey = DeparturePlace + PointOfInterest + LayoverTime + AreaStatus;
            var FilterData = _allDataExchangethroughRedisCache.GetDataFromCache<List<PlaceAttributes>>(FilterKey) == null ?
            await GetFilterData(await _getSearch.GetAllData(DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest,AreaStatus), DeparturePlace, LayoverTime, FilterKey)
            : _allDataExchangethroughRedisCache.GetDataFromCache<List<PlaceAttributes>>(FilterKey);

            if (FilterData != null)
                return Ok(FilterData);
            else
                return BadRequest("Data Not Found");
        }
        //GET: api/Data/outsideAirport
        [HttpGet("outsideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}/{LayoverTime}")]
        public async Task<IActionResult> GetOutsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest,int LayoverTime)
        {
            string FilterKey = DeparturePlace + PointOfInterest + LayoverTime + "Outside";
            var FilterData = _allDataExchangethroughRedisCache.GetDataFromCache<List<PlaceAttributes>>(FilterKey) == null ?
            await GetFilterData(await _getAllData.GetAllData(DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest), DeparturePlace, LayoverTime, FilterKey)
            : _allDataExchangethroughRedisCache.GetDataFromCache<List<PlaceAttributes>>(FilterKey);

            if (FilterData != null)
                return Ok(FilterData);
            else
                return BadRequest("Data Not Found");
        }

        //GET: api/Data/insideAirport
        [HttpGet("insideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}/{LayoverTime}")]

        //PointOfInterest is any Stores/Restorents...etc
        public async Task<IActionResult> GetInsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest,int LayoverTime)
        {
            string FilterKey = DeparturePlace + PointOfInterest + LayoverTime + "Inside";
            var FilterData = _allDataExchangethroughRedisCache.GetDataFromCache<List<PlaceAttributes>>(FilterKey) == null ?
            await GetFilterData(await _getData.GetData(_getLatitudeLongitude.Get(DeparturePlace), DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest), DeparturePlace, LayoverTime, FilterKey)
            : _allDataExchangethroughRedisCache.GetDataFromCache<List<PlaceAttributes>>(FilterKey);

            if (FilterData != null)
                return Ok(FilterData);
            else
                return BadRequest("Data Not Found");
        }

        //GET: api/Data/InsideOutsideAirport
        [HttpGet("InsideOutsideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}/{LayoverTime}")]
        public async Task<IActionResult> GetInsideOutsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest,int LayoverTime)
        {
            string FilterKey = DeparturePlace + PointOfInterest + LayoverTime + "InsideOutside";
            var FilterData = _allDataExchangethroughRedisCache.GetDataFromCache<List<PlaceAttributes>>(FilterKey) == null ?
            await GetFilterData(await _getInsideOutsideData.GetInsideOutsideData(_getLatitudeLongitude.Get(DeparturePlace), DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest), DeparturePlace, LayoverTime, FilterKey)
            : _allDataExchangethroughRedisCache.GetDataFromCache<List<PlaceAttributes>>(FilterKey);

            if (FilterData != null)
                return Ok(FilterData);
            else
                return BadRequest("Data Not Found");
        }
        private async Task<List<PlaceAttributes>> GetFilterData(List<PlaceAttributes> AllData, string DeparturePlace, int LayoverTime,string FilterKey)
        {
            var data = await _getDataAccordingToLayoverTime.GetFilterData(AllData, DeparturePlace, LayoverTime, FilterKey);
            if (data != null)
                return data;
            else
                return null;
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
            string FilterKey = PlaceID;
            var Data= _allDataExchangethroughRedisCache.GetDataFromCache<PlaceAttributes>(FilterKey) == default(PlaceAttributes) ?
                await _getPlaceData.GetPlaceData(DeparturePlace,PlaceID): 
                _allDataExchangethroughRedisCache.GetDataFromCache<PlaceAttributes>(FilterKey);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Not Found");
        }
        [HttpGet("reminder/{phoneNumber}/{placeId}/{name}/{distance}/{storeNumber}")]
        public void SetReminder(string phoneNumber, string placeId, string name, string distance, string storeNumber, string returnUrl)
        {

            _setReminderData.SetReminderForIternary(phoneNumber, placeId, name, distance, storeNumber, returnUrl);
        }
        [HttpGet("reminder/{phoneNumber}")]
        public void SetReminder(string phoneNumber, string returnUrl)
        {
            _setReminderData.SetReminderForAll(phoneNumber, returnUrl);
        }
    }
}
