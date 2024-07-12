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
            var actualStartTime = new TimeOnly(11, 0, 0);
            var actualEndTime = new TimeOnly(11, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(actualStartTime, result.StartTime);
            Assert.Equal(actualEndTime, result.EndTime);
        }

        [Fact]
        public void ShouldReturnFalseIfActualTimeRangeOutsideRuleTimeRange()
        {
            var actualStartTime = new TimeOnly(9, 0, 0);
            var actualEndTime = new TimeOnly(9, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
        }

        [Fact]
        public void ShouldFitResultStartTimeBeActualStartTimeIfActualStartTimeIsAfterRuleStartTime()
        {
            var actualStartTime = new TimeOnly(11, 0, 0);
            var actualEndTime = new TimeOnly(13, 30, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.Equal(actualStartTime, result.StartTime);
            Assert.Equal(ruleEndTime, result.EndTime);
        }

        [Fact]
        public void ShouldFitResultEndTimeBeActualEndTimeIfActualEndTimeIsBeforeRuleEndTime()
        {
            var actualStartTime = new TimeOnly(9, 0, 0);
            var actualEndTime = new TimeOnly(11, 30, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.Equal(ruleStartTime, result.StartTime);
            Assert.Equal(actualEndTime, result.EndTime);
        }

        [Fact]
        public void ShouldReturnFalseIfTheActualStartTimeIsAfterRuleEndTime() {
            var actualStartTime = new TimeOnly(13, 0, 0);
            var actualEndTime = new TimeOnly(13, 30, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit); 
        }

        [Fact]
        public void ShouldThrowNotImplementExceptionWhenCalculateCostIsCalled()
        {
            var actualStartTime = new TimeOnly(11, 0, 0);
            var actualEndTime = new TimeOnly(11, 30, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.Throws<NotImplementedException>(() => feeCalculator.CalculateCost(result));
        }
    }
}