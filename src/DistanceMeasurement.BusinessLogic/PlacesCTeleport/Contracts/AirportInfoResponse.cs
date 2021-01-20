using System;
using System.Collections.Generic;
using System.Text;

namespace DistanceMeasurement.BusinessLogic.PlacesCTeleport.Contracts
{
    public class AirportInfoResponse
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool Success { get; set; }
    }
}
