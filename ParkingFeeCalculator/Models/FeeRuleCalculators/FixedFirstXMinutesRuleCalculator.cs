using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Models.FeeRuleCalculators
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
            // task #1
            // TODO: implement the calculateCost method
            var result = 0m;
            return result.ToTwoDecimalPlaces();
        }
    }
}