using ParkingFeeCalculator.Services.FeeRuleCalculators;

namespace ParkingFeeCalculator.Tests.Services.FeeRuleCalculators
{
    public class FixedFeePerXMinutesRuleCalculatorTest
    {

        FixedFeePerXMinutesRuleCalculator feeCalculator = new FixedFeePerXMinutesRuleCalculator(new TimeOnly(10, 0), new TimeOnly(16, 0), 15, 0.5m);

        [Fact]
        public void ShouldReturn2For60Minutes()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(2, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturn0IfNotFitForCalculateCost()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 9, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
            Assert.Equal(0, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturn12From10To16()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 16, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(12, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnBaseFeeForSameStartTimeAndEndTime()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(0.5m, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturn12For10To17()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 17, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(12, feeCalculator.CalculateCost(result));
        }
    }
}
