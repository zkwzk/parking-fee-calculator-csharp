using System;
using ParkingFeeCalculator.Services;
using Xunit;
using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Tests.Services
{
    public class RuleCalculatorBaseTest
    {
        [Fact]
        public void ShouldReturnTrueIfActualTimeRangeWithinRuleTimeRange()
        {
            var ruleStartTime = new TimeOnly(10, 0);
            var ruleEndTime = new TimeOnly(12, 0);
            var actualStartTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 30, 0, 0);
            var feeCalculatorBase = new RuleCalculatorBase(ruleStartTime, ruleEndTime);
            var result = feeCalculatorBase.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(actualStartTime.ToTimeOnly(), result.StartTime);
            Assert.Equal(actualEndTime.ToTimeOnly(), result.EndTime);
        }

        [Fact]
        public void ShouldReturnFalseIfActualTimeRangeOutsideRuleTimeRange()
        {
            var ruleStartTime = new TimeOnly(10, 0);
            var ruleEndTime = new TimeOnly(12, 0);
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 9, 30, 0, 0);
            var feeCalculatorBase = new RuleCalculatorBase(ruleStartTime, ruleEndTime);
            var result = feeCalculatorBase.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
        }

        [Fact]
        public void ShouldFitResultStartTimeBeActualStartTimeIfActualStartTimeIsGreaterThanRuleStartTime()
        {
            var ruleStartTime = new TimeOnly(10, 0);
            var ruleEndTime = new TimeOnly(12, 0);
            var actualStartTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 13, 30, 0, 0);
            var feeCalculatorBase = new RuleCalculatorBase(ruleStartTime, ruleEndTime);
            var result = feeCalculatorBase.IsFit(actualStartTime, actualEndTime);
            Assert.Equal(actualStartTime.ToTimeOnly(), result.StartTime);
            Assert.Equal(ruleEndTime, result.EndTime);
        }

        [Fact]
        public void ShouldFitResultEndTimeBeActualEndTimeIfActualEndTimeIsLessThanRuleEndTime()
        {
            var ruleStartTime = new TimeOnly(10, 0);
            var ruleEndTime = new TimeOnly(12, 0);
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 30, 0, 0);
            var feeCalculatorBase = new RuleCalculatorBase(ruleStartTime, ruleEndTime);
            var result = feeCalculatorBase.IsFit(actualStartTime, actualEndTime);
            Assert.Equal(ruleStartTime, result.StartTime);
            Assert.Equal(actualEndTime.ToTimeOnly(), result.EndTime);
        }

        [Fact]
        public void ShouldFitResultBeEntireRuleTimeRangeIfActualTimeRangeIsCoverRuleTimeRange()
        {
            var ruleStartTime = new TimeOnly(10, 0);
            var ruleEndTime = new TimeOnly(12, 0);
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            
            var actualEndTime = new DateTime(2024, 3, 20, 12, 0, 1);
            var feeCalculatorBase = new RuleCalculatorBase(ruleStartTime, ruleEndTime);
            var result = feeCalculatorBase.IsFit(actualStartTime, actualEndTime);
            Assert.Equal(ruleStartTime, result.StartTime);
            Assert.Equal(ruleEndTime, result.EndTime);
        }
    }
}