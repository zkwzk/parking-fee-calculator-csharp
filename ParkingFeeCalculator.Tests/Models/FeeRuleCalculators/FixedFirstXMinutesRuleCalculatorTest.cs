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
        public void ShouldReturnFirstXMinutesFeeAndSubsequenceYMinutesFeeIfActualTimeRangeExceedsXMinutesByOneMinute()
        {
            var actualStartTime = new TimeOnly(10, 0);
            var actualEndTime = new TimeOnly(12, 1);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(ruleXMinutesFee + ruleYMinutesFee, feeCalculator.CalculateCost(result));
        }

        // task #1
        // TODO: implement more test cases for the calculateCost method
    }
}