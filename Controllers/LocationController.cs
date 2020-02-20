using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConvertIPAddressToLocation.Helpers;
using ConvertIPAddressToLocation.model;

namespace ConvertIPAddressToLocation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        // GET: api/Location
        [HttpGet]
        public IEnumerable<Symbol> Get([FromQuery] string date, [FromQuery] string country)
        {
            LocationDAL locationDAL= new LocationDAL();
            IEnumerable<Symbol> result = locationDAL.getLocations(date, country);
            return result;
        }

        // POST: api/Location
        [HttpPost]
        public Symbol Post([FromQuery] string ipAddress)
        {
            GeoLocation geoLocation = new GeoLocation();
            Symbol result = geoLocation.FetchIPLocation(ipAddress);
            if (result != null)
            {
                LocationDAL locationDAL = new LocationDAL();
                locationDAL.saveSymbolToDB(result);
            }
            //followed aws instructions
           //SMSService smsService = new SMSService();
           //smsService.sendSMS();
            return result;

        }

    }
}
