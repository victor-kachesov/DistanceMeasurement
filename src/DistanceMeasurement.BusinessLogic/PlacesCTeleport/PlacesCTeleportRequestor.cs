using DistanceMeasurement.BusinessLogic.PlacesCTeleport.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DistanceMeasurement.BusinessLogic.PlacesCTeleport
{
    public class PlacesCTeleportRequestor: IPlacesCTeleportRequestor
    {
        public PlacesCTeleportRequestor(IPlacesCTeleportClient placesClient, IMemoryCache cache, ILogger<PlacesCTeleportRequestor> logger)
        {
            _placesClient = placesClient;
            _cache = cache;
            _logger = logger;
        }

        private readonly IPlacesCTeleportClient _placesClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<PlacesCTeleportRequestor> _logger;
        private readonly TimeSpan _expireTime = TimeSpan.FromDays(1);

        public async Task<AirportInfoResponse> GetAirportInfoAsync(AirportInfoRequest request)
        {
            try
            {
                string key = GetKey(request.Code);

                if (_cache.TryGetValue(key, out AirportInfoResponse cachedResponse))
                {
                    return cachedResponse;
                }
                
                var airportInfo = await _placesClient.GetAirportInfoAsync(request.Code);

                var response = new AirportInfoResponse {
                    Success = true,
                    Lat = airportInfo.location.lat,
                    Lon = airportInfo.location.lon
                };

                _cache.Set(key, response, _expireTime);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't get the airport info.");

                return new AirportInfoResponse {
                    Success = false,
                };
            }
        }

        private string GetKey(string code)
        {
            return $"AIRPORT:{code}";
        }
    }
}
