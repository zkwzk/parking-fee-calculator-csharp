using ParkingFeeCalculator.Models.FeeRuleCalculators;

namespace ParkingFeeCalculator.Tests.Models.FeeRuleCalculators
{
    public class FixedFeePerEntryRuleCalculatorTest
    {
        static TimeOnly ruleStartTime = new TimeOnly(10, 0);
        static TimeOnly ruleEndTime = new TimeOnly(12, 0);
        static decimal ruleFee = 5;
        
        FixedFeePerEntryRuleCalculator feeCalculator = new FixedFeePerEntryRuleCalculator(ruleStartTime, ruleEndTime, ruleFee);

        [Fact]
        public void ShouldReturnRuleFeeIfActualTimeRangeWithinRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(11, 0, 0);
            var actualEndTime = new TimeOnly(11, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleFee, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturnZeroIfActualTimeRangeBeforeRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(9, 0, 0);
            var actualEndTime = new TimeOnly(9, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
            Assert.Equal(0, feeCalculator.CalculateCost(result));
        }
    }
}