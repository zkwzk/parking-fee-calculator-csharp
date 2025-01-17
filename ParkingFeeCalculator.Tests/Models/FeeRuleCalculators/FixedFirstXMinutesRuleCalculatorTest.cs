using ParkingFeeCalculator.Models.FeeRuleCalculators;

namespace ParkingFeeCalculator.Tests.Models.FeeRuleCalculators
{
    public class FixedFirstXMinutesRuleCalculatorTest
    {
        static TimeOnly ruleStartTime = new TimeOnly(10, 0);
        static TimeOnly ruleEndTime = new TimeOnly(16, 0);
        static int ruleXMinutes = 120;
        static int ruleYMinutes = 15;
        static decimal ruleXMinutesFee = 5;
        static decimal ruleYMinutesFee = 0.55m;

        FixedFirstXMinutesRuleCalculator feeCalculator = new FixedFirstXMinutesRuleCalculator(ruleStartTime, ruleEndTime, ruleXMinutes, ruleYMinutes, ruleXMinutesFee, ruleYMinutesFee);

        [Fact]
        public void ShouldReturnFirstXMinutesFeeIfActualTimeRangeWithinXMinutes()
        {
            var actualStartTime = new TimeOnly(10, 0);
            var actualEndTime = new TimeOnly(11, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnZeroIfActualTimeRangeBeforeRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(9, 0);
            var actualEndTime = new TimeOnly(9, 30);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
            Assert.Equal(0, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeIfActualStartTimeAndActualEndTimeAreSame()
        {
            var actualStartTime = new TimeOnly(10, 0);
            var actualEndTime = new TimeOnly(10, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeIfActualTimeRangeExactlyMatchesXMinutes()
        {
            var actualStartTime = new TimeOnly(10, 0);
            var actualEndTime = new TimeOnly(12, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeAndSubsequenceYMinutesFeeIfActualTimeRangeExceedsXMinutesByOneMinute()
        {
            var actualStartTime = new TimeOnly(10, 0);
            var actualEndTime = new TimeOnly(12, 1);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee + ruleYMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeAndSubsequenceYMinutesFeeIfActualTimeRangeExceedsXMinutes()
        {
            var actualStartTime = new TimeOnly(10, 0);
            var actualEndTime = new TimeOnly(13, 7);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee + (ruleYMinutesFee * 5m), feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeAndSubsequenceYMinutesFeeIfActualTimeRangeExactlyMatchesRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(10, 0);
            var actualEndTime = new TimeOnly(16, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee + (ruleYMinutesFee * 16m), feeCalculator.CalculateCost(result));
        }
    }
}