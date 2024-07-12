using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Models.FeeRuleCalculators
{
    public class FixedFeePerXMinutesRuleCalculator : RuleCalculatorBase
    {
        public int XMinutes { get; private set; }
        public decimal XMinutesFee { get; private set; }

        public FixedFeePerXMinutesRuleCalculator(TimeOnly startTime, TimeOnly endTime, int xMinutes, decimal xMinutesFee) : base(startTime, endTime)
        {
            this.XMinutes = xMinutes;
            this.XMinutesFee = xMinutesFee;
        }

        public override decimal CalculateCost(FitResult fitResult)
        {
            if (!fitResult.IsFit)
            {
                return 0;
            }

            if (fitResult.StartTime == fitResult.EndTime)
            {
                return this.XMinutesFee;
            }

            var totalMinutes = (fitResult.EndTime - fitResult.StartTime).TotalMinutes;
            var chargeableXMinutes = Math.Ceiling(totalMinutes / this.XMinutes);
            return (this.XMinutesFee * (decimal)chargeableXMinutes).ToTwoDecimalPlaces();
        }
    }
}