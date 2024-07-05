using ParkingFeeCalculator.Services;
using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Tests.Services
{
    public class RuleCalculatorBaseTest
    {
        static TimeOnly ruleStartTime = new TimeOnly(10, 0);
        static TimeOnly ruleEndTime = new TimeOnly(12, 0);
        
        RuleCalculatorBase feeCalculator = new RuleCalculatorBase(ruleStartTime, ruleEndTime);

        [Fact]
        public void ShouldReturnTrueIfActualTimeRangeWithinRuleTimeRange()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(actualStartTime.ToTimeOnly(), result.StartTime);
            Assert.Equal(actualEndTime.ToTimeOnly(), result.EndTime);
        }

        [Fact]
        public void ShouldReturnFalseIfActualTimeRangeOutsideRuleTimeRange()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 9, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
        }

        [Fact]
        public void ShouldFitResultStartTimeBeActualStartTimeIfActualStartTimeIsAfterRuleStartTime()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 13, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.Equal(actualStartTime.ToTimeOnly(), result.StartTime);
            Assert.Equal(ruleEndTime, result.EndTime);
        }

        [Fact]
        public void ShouldFitResultEndTimeBeActualEndTimeIfActualEndTimeIsBeforeRuleEndTime()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.Equal(ruleStartTime, result.StartTime);
            Assert.Equal(actualEndTime.ToTimeOnly(), result.EndTime);
        }

        [Fact]
        public void ShouldReturnFalseIfTheActualStartTimeIsAfterRuleEndTime() {
            var actualStartTime = new DateTime(2024, 3, 20, 13, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 13, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit); 
        }

        [Fact]
        public void ShouldThrowNotImplementExceptionWhenCalculateCostIsCalled()
        {
            var actualStartTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.Throws<NotImplementedException>(() => feeCalculator.CalculateCost(result));
        }
    }
}