using ParkingFeeCalculator.Models;

namespace ParkingFeeCalculator.Services.FeeRuleCalculators
{
    public class FixedFeePerEntryRuleCalculator : RuleCalculatorBase
    {
        public decimal Fee { get; set; }

        public FixedFeePerEntryRuleCalculator(TimeOnly startTime, TimeOnly endTime, decimal fee) : base(startTime, endTime)
        {
            this.Fee = fee;
        }

        public override decimal CalculateCost(FitResult fitResult)
        {
            if(!fitResult.IsFit) {
                return 0;
            }
            
            return this.Fee;
        }
    }
}