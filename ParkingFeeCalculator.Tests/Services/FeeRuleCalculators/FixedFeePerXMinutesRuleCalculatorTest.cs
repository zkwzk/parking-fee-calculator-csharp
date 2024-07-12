using ParkingFeeCalculator.Services.FeeRuleCalculators;

namespace ParkingFeeCalculator.Tests.Services.FeeRuleCalculators
{
    public class FixedFeePerXMinutesRuleCalculatorTest
    {
        static TimeOnly ruleStartTime = new TimeOnly(10, 0);
        static TimeOnly ruleEndTime = new TimeOnly(16, 0);
        static int ruleXMinutes = 15;
        static decimal ruleXMinutesFee = 0.5m;
        
        FixedFeePerXMinutesRuleCalculator feeCalculator = new FixedFeePerXMinutesRuleCalculator(ruleStartTime, ruleEndTime, ruleXMinutes, ruleXMinutesFee);

        [Fact]
        public void ShouldReturnXMinutesFeeIfActualTimeRangeWithinRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(10, 0, 0);
            var actualEndTime = new TimeOnly(11, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee * 4m, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnZeroIfActualTimeRangeBeforeRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(9, 0, 0);
            var actualEndTime = new TimeOnly(9, 30, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
            Assert.Equal(0, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnXMinutesFeeIfActualStartTimeAndActualEndTimeAreSame()
        {
            var actualStartTime = new TimeOnly(10, 0, 0);
            var actualEndTime = new TimeOnly(10, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnRoundedXMinutesFeeIfActualTimeRangeExceedsRuleTimeRangeByOneMinute()
        {
            var actualStartTime = new TimeOnly(10, 0, 0);
            var actualEndTime = new TimeOnly(10, 16, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee * 2m, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnXMinutesFeeIfActualTimeRangeExactlyMatchesRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(10, 0, 0);
            var actualEndTime = new TimeOnly(16, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee * 24m, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnXMinutesFeeIfActualTimeRangeExceedsRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(10, 0, 0);
            var actualEndTime = new TimeOnly(17, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee * 24m, feeCalculator.CalculateCost(result));
        }
    }
}
