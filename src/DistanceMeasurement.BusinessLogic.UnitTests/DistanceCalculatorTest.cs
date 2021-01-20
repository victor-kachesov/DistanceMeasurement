using DistanceMeasurement.BusinessLogic.DistanceCalculator;
using System;
using Xunit;

namespace DistanceMeasurement.BusinessLogic.UnitTests
{
    public class DistanceCalculatorTest
    {
        public DistanceCalculatorTest()
        {
            _distanceCalculator = new DistanceCalculator.DistanceCalculator();
        }

        private readonly IDistanceCalculator _distanceCalculator;

        [Fact]
        public void Calculate_CorrectCoordinates_ReturnsRightDistance()
        {
            // Arrange
            double fromLat = 0;
            double fromLon = 0;
            double toLat = 90;
            double toLon = 0;

            // Act
            double distance = _distanceCalculator.Calculate(fromLat, fromLon, toLat, toLon);

            // Assert
            Assert.Equal(10010362.98, distance, 2);
        }

        [Fact]
        public void Calculate_FromDmeToSvoCoordinates_ReturnsRightDistance()
        {
            // Arrange
            double dmeLat = 55.414566;
            double dmeLon = 37.899494;
            double svoLat = 55.966324;
            double svoLon = 37.416574;

            // Act
            double distance = _distanceCalculator.Calculate(dmeLat, dmeLon, svoLat, svoLon);

            // Assert
            Assert.Equal(68431.51, distance, 2);
        }
        
        [Fact]
        public void Calculate_FromAmsToJfkCoordinates_ReturnsRightDistance()
        {
            // Arrange
            double amsLat = 52.309069;
            double amsLon = 4.763385;
            double jfkLat = 40.642335;
            double jfkLon = -73.78817;

            // Act
            double distance = _distanceCalculator.Calculate(amsLat, amsLon, jfkLat, jfkLon);

            // Assert
            Assert.Equal(5849525.21, distance, 2);
        }
    }
}
