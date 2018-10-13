using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThingsToDoProject.DataAccess;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsideAirportController : ControllerBase
    {
        // GET: api/InsideAirport
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string City = "Pune";
            Location Position = GetLatitudeLongitudeOfParticularCity.GetLatitudeLogitude(City);
            return new string[] { "value1", "value2" };
        }

        // GET: api/InsideAirport/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InsideAirport
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/InsideAirport/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
