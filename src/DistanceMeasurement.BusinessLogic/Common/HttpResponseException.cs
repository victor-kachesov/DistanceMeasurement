using System;
using System.Collections.Generic;
using System.Text;

namespace DistanceMeasurement.BusinessLogic.Common
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
