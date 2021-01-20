using DistanceMeasurement.BusinessLogic.PlacesCTeleport.Contracts;
using DistanceMeasurement.BusinessLogic.PlacesCTeleport.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DistanceMeasurement.BusinessLogic.PlacesCTeleport
{
    public interface IPlacesCTeleportClient
    {
        Task<AirportInfoDto> GetAirportInfoAsync(string code);
    }
}
