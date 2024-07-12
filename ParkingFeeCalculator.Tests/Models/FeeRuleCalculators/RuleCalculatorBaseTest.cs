using ParkingFeeCalculator.Models.FeeRuleCalculators;

namespace ParkingFeeCalculator.Tests.Models.FeeRuleCalculators
{
    public class RuleCalculatorBaseTest
    {
        static TimeOnly ruleStartTime = new TimeOnly(10, 0);
        static TimeOnly ruleEndTime = new TimeOnly(12, 0);

        RuleCalculatorBase feeCalculator = new RuleCalculatorBase(ruleStartTime, ruleEndTime);

        [Fact]
        public void ShouldReturnTrueIfActualTimeRangeWithinRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(11, 0);
            var actualEndTime = new TimeOnly(11, 30);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(actualStartTime, result.StartTime);
            Assert.Equal(actualEndTime, result.EndTime);
        }

        // task #2
        // TODO: implement more test cases for the isFit method

        [Fact]
        public void ShouldThrowNotImplementExceptionWhenCalculateCostIsCalled()
        {
            var actualStartTime = new TimeOnly(11, 0);
            var actualEndTime = new TimeOnly(11, 30);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.Throws<NotImplementedException>(() => feeCalculator.CalculateCost(result));
        }
    }
}