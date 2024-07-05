using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Services.FeeRuleCalculators
{
    public class FixedFirstXMinutesRuleCalculator : RuleCalculatorBase
    {
        public int XMinutes { get; private set; }
        public int YMinutes { get; private set; }
        public decimal FirstXMinutesFee { get; private set; }
        public decimal SubsequenceYMinutesFee { get; private set; }

        public FixedFirstXMinutesRuleCalculator(TimeOnly startTime, TimeOnly endTime, int xMinutes, int yMinutes, decimal firstXMinutesFee, decimal subsequenceYMinutesFee) : base(startTime, endTime)
        {
            this.XMinutes = xMinutes;
            this.YMinutes = yMinutes;
            this.FirstXMinutesFee = firstXMinutesFee;
            this.SubsequenceYMinutesFee = subsequenceYMinutesFee;
        }

        public override decimal CalculateCost(FitResult fitResult)
        {
            if (!fitResult.IsFit)
            {
                return 0;
            }

            if (fitResult.StartTime == fitResult.EndTime)
            {
                return this.FirstXMinutesFee;
            }

            var totalMinutes = (fitResult.EndTime - fitResult.StartTime).TotalMinutes;

            if (totalMinutes <= this.XMinutes)
            {
                return FirstXMinutesFee;
            }

            var durationAfterXMinutes = totalMinutes - this.XMinutes;
            var chargeableYMinutes = Math.Ceiling(durationAfterXMinutes / this.YMinutes);
            return (this.FirstXMinutesFee + (this.SubsequenceYMinutesFee * (decimal)chargeableYMinutes)).ToTwoDecimalPlaces();
        }
    }
}