using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Tests.Utilities
{
    public class DecimalUtilityTest
    {
        [Fact]
        public void ShouldReturn2DecimalPlaces()
        {
            decimal value = 1.23456789m;
            var result = value.To2DecimalPlaces();
            Assert.Equal(1.23m, result);
        }
    }
}