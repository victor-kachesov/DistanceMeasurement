using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistanceMeasurement.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DistanceMeasurement.Controllers
{
    [Route("api/distance")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        public DistanceController(MeasureDistanceRequestHandler measureDistanceRequestHandler)
        {
            _measureDistanceRequestHandler = measureDistanceRequestHandler;
        }

        private readonly MeasureDistanceRequestHandler _measureDistanceRequestHandler;

        // GET api/distance/{fromCode}/{toCode}
        [HttpGet("{fromCode}/{toCode}")]
        public async Task<double> GetAsync(string fromCode, string toCode)
        {
            double distance = await _measureDistanceRequestHandler.GetDistanceAsync(fromCode, toCode);

            return distance;
        }
    }
}
