using DistanceMeasurement.BusinessLogic.Common;
using DistanceMeasurement.BusinessLogic.DistanceCalculator;
using DistanceMeasurement.BusinessLogic.PlacesCTeleport;
using DistanceMeasurement.BusinessLogic.PlacesCTeleport.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DistanceMeasurement.BusinessLogic
{
    public class MeasureDistanceRequestHandler
    {
        public MeasureDistanceRequestHandler(
            IPlacesCTeleportRequestor placesRequestor,
            IDistanceCalculator distanceCalculator,
            IMemoryCache cache)
        {
            _placesRequestor = placesRequestor;
            _distanceCalculator = distanceCalculator;
            _cache = cache;
        }

        private readonly IPlacesCTeleportRequestor _placesRequestor;
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _expireTime = TimeSpan.FromDays(1);

        public async Task<double> GetDistanceAsync(string airportFromCode, string airportToCode)
        {
            if (TryGetFromCache(airportFromCode, airportToCode, out double cachedDistance))
            {
                return cachedDistance;
            }
            
            var airportFromRequest = new AirportInfoRequest { 
                Code = airportFromCode
            };

            var airportFromResponse = await _placesRequestor.GetAirportInfoAsync(airportFromRequest);
            if (!airportFromResponse.Success)
            {
                throw new HttpResponseException
                {
                    Status = (int)HttpStatusCode.NotFound
                };
            }

            var airportToRequest = new AirportInfoRequest {
                Code = airportToCode
            };

            var airportToResponse = await _placesRequestor.GetAirportInfoAsync(airportToRequest);
            if (!airportToResponse.Success)
            {
                throw new HttpResponseException
                {
                    Status = (int)HttpStatusCode.NotFound
                };
            }

            double distance = _distanceCalculator.Calculate(airportFromResponse.Lat, airportFromResponse.Lon, airportToResponse.Lat, airportToResponse.Lon);

            string key = GetKey(airportFromCode, airportToCode);

            _cache.Set(key, distance, _expireTime);

            return distance;
        }

        private bool TryGetFromCache(string airportFromCode, string airportToCode, out double distance)
        {
            string key = GetKey(airportFromCode, airportToCode);

            if (_cache.TryGetValue(key, out distance))
            {
                return true;
            }

            string reversedKey = GetKey(airportToCode, airportFromCode);

            if (_cache.TryGetValue(reversedKey, out distance))
            {
                return true;
            }

            return false;
        }

        private string GetKey(string airportFromCode, string airportToCode)
        {
            return $"DIST:{airportFromCode}:{airportToCode}";
        }
    }
}
