using ParkingFeeCalculator.Services.FeeRuleCalculators;

namespace ParkingFeeCalculator.Tests.Services.FeeRuleCalculators
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
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnZeroIfActualTimeRangeBeforeRuleTimeRange()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 9, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
            Assert.Equal(0, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeIfActualStartTimeAndActualEndTimeAreSame()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeIfActualTimeRangeExactlyMatchesXMinutes()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 12, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeAndSubsequenceYMinutesFeeIfActualTimeRangeExceedsXMinutesByOneMinute()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 12, 1, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee + ruleYMinutesFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeAndSubsequenceYMinutesFeeIfActualTimeRangeExceedsXMinutes()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 13, 7, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee + (ruleYMinutesFee * 5m), feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnFirstXMinutesFeeAndSubsequenceYMinutesFeeIfActualTimeRangeExactlyMatchesRuleTimeRange()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 10, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 16, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee + (ruleYMinutesFee * 16m), feeCalculator.CalculateCost(result));
        }
    }
}