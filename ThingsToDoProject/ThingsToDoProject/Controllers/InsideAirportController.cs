using System;
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
    public class InsideAirportController : ControllerBase
    {
        private readonly IGetData _getData;

        public InsideAirportController(IGetData getData)
        {
            _getData = getData;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string City = "Pune Airport";
            string TypeValue = "store";
            GetLatitudeLongitude PositionObject = new GetLatitudeLongitude();
            //Location Position = PositionObject.Get(City);
            Location Position = null;
            var Data = await _getData.GetData(Position, TypeValue);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Not Found");
        }
        //[HttpGet]
        //// GET: api/InsideAirport
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    string City = "Pune";
        //    string typevalue = "store";
        //    //Location Position = GetLatitudeLongitudeOfParticularCity.GetLatitudeLogitude(City);
        //    List<DataAttributes> Data = DataAccess.GetDataOfParticularType.GetAllDataOfParticularType(typevalue);
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/InsideAirport/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/InsideAirport
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/InsideAirport/5
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
