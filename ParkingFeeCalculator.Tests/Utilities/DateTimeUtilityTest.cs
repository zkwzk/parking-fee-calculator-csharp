using System;
using Xunit;
using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Tests.Utilities
{
    public class DateTimeUtilityTest
    {
        [Fact]
        public void ShouldConvertToTimeOnlySuccessfully()
        {
            var dateTime = new DateTime(2024, 3, 20, 11, 1, 2, 3);
            var expectTimeOnly = new TimeOnly(11, 1, 2, 3);

            Assert.Equal(expectTimeOnly, dateTime.ToTimeOnly());
        }
    }
}