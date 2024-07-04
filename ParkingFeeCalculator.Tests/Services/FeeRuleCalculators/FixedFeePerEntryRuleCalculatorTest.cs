using ParkingFeeCalculator.Services.FeeRuleCalculators;

namespace ParkingFeeCalculator.Tests.Services.FeeRuleCalculators
{
    public class FixedFeePerEntryRuleCalculatorTest {
        static TimeOnly ruleStartTime = new TimeOnly(10, 0);
        static TimeOnly ruleEndTime = new TimeOnly(12, 0);
        FixedFeePerEntryRuleCalculator feeCalculator = new FixedFeePerEntryRuleCalculator(ruleStartTime, ruleEndTime, 10);

        [Fact]
        public void ShouldReturnFalseIfNotFit() {
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 9, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
        }

        [Fact]
        public void ShouldReturn10IfFit() {
            var actualStartTime = new DateTime(2024, 3, 20, 11, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 11, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.True(result.IsFit);
            Assert.Equal(10, feeCalculator.CalculateCost(result));
        }

        [Fact]
        public void ShouldReturn0IfNotFitForCalculateCost() {
            var actualStartTime = new DateTime(2024, 3, 20, 9, 0, 0);
            var actualEndTime = new DateTime(2024, 3, 20, 9, 30, 0, 0);
            var result = feeCalculator.IsFit(actualStartTime, actualEndTime);
            Assert.False(result.IsFit);
            Assert.Equal(0, feeCalculator.CalculateCost(result));
        }
    }
}