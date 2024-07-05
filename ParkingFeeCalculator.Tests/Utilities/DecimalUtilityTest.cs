using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Tests.Utilities
{
    public class DecimalUtilityTest
    {
        [Fact]
        public void ShouldReturnTwoDecimalPlaces()
        {
            decimal value = 1.23456789m;
            var result = value.ToTwoDecimalPlaces();
            Assert.Equal(1.23m, result);
        }
    }
}