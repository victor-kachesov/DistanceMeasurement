using System;
using System.Collections.Generic;
using System.Text;

namespace DistanceMeasurement.BusinessLogic.DistanceCalculator
{
    public class DistanceCalculator : IDistanceCalculator
    {
        private const double EARTH_RADIUS = 6372795;

        public double Calculate(double latD1, double lonD1, double latD2, double lonD2)
        {
            // convert to radians
            double lat1 = latD1 * Math.PI / 180;
            double lat2 = latD2 * Math.PI / 180;
            double lon1 = lonD1 * Math.PI / 180;
            double lon2 = lonD2 * Math.PI / 180;

            double ad = Haversine(lat1, lon1, lat2, lon2);

            double distance = ad * EARTH_RADIUS;

            return distance;
        }

        private double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            double cl1 = Math.Cos(lat1);
            double cl2 = Math.Cos(lat2);
            double sl1 = Math.Sin(lat1);
            double sl2 = Math.Sin(lat2);

            double delta = lon2 - lon1;
            double cdelta = Math.Cos(delta);
            double sdelta = Math.Sin(delta);

            double y = Math.Sqrt(Math.Pow(cl2 * sdelta, 2) + Math.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));
            
            double x = sl1 * sl2 + cl1 * cl2 * cdelta;

            double result = Math.Atan2(y, x);

            return result;
        }
    }
}
