using ParkingFeeCalculator.Services.FeeRuleCalculators;

namespace ParkingFeeCalculator.Tests.Services.FeeRuleCalculators
{
    public class FixedFirstXMinutesRuleCalculatorTest
    {
        static TimeOnly ruleStartTime = new TimeOnly(10, 0);
        static TimeOnly ruleEndTime = new TimeOnly(16, 0);
        FixedFirstXMinutesRuleCalculator feeCalculator = new FixedFirstXMinutesRuleCalculator(ruleStartTime, ruleEndTime, 120, 15, 5, 0.55m);

        [Fact]
        public void ShouldReturn5For60Minutes()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(5, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnCalculatedFeeFrom10To137()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 13, 7, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(7.75m, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturn0IfNotFit()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 9, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
        }

        [Fact]
        public void ShouldReturn5IfStartTimeAndEndTimeSame()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(5, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturn5For120Minutes()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 12, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(5, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturn13_8ForFrom10To16()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 16, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(13.8m, feeCalculator.CalculateCost(result));
        }
    }
}