using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Services.FeeRuleCalculators
{
    public class FixedFirstXMinutesRuleCalculator : RuleCalculatorBase
    {
        public decimal FirstXMinutesFee { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public decimal SubSequenceChargeFeePerYMinutes { get; set; }

        public FixedFirstXMinutesRuleCalculator(TimeOnly startTime, TimeOnly endTime, int x, int y, decimal firstXMinutesFee, decimal subSequenceChargeFeePerYMinutes) : base(startTime, endTime)
        {
            this.FirstXMinutesFee = firstXMinutesFee;
            this.X = x;
            this.Y = y;
            this.SubSequenceChargeFeePerYMinutes = subSequenceChargeFeePerYMinutes;
        }

        public override decimal CalculateCost(FitResult fitResult)
        {
            if(!fitResult.IsFit) {
                return 0;
            }

             if (fitResult.StartTime == fitResult.EndTime)
            {
                return FirstXMinutesFee;
            }

            var totalMinutes = (fitResult.EndTime - fitResult.StartTime).TotalMinutes;

            if (totalMinutes <= X)
            {
                return FirstXMinutesFee;
            }

            var duration = totalMinutes - X;
            var timeCeil = System.Math.Ceiling(duration / Y);
            return (FirstXMinutesFee + (SubSequenceChargeFeePerYMinutes * (decimal)timeCeil)).To2DecimalPlaces();
        }
    }
}