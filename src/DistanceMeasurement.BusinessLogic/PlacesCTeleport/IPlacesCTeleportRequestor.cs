using DistanceMeasurement.BusinessLogic.PlacesCTeleport.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DistanceMeasurement.BusinessLogic.PlacesCTeleport
{
    public interface IPlacesCTeleportRequestor
    {
        Task<AirportInfoResponse> GetAirportInfoAsync(AirportInfoRequest request);
    }
}
