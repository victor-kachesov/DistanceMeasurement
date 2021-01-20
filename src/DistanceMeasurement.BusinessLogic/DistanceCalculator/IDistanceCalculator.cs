using System;
using System.Collections.Generic;
using System.Text;

namespace DistanceMeasurement.BusinessLogic.DistanceCalculator
{
    public interface IDistanceCalculator
    {
        double Calculate(double latD1, double lonD1, double latD2, double lonD2);
    }
}
