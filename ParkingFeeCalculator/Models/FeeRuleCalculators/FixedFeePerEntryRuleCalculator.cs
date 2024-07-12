namespace ParkingFeeCalculator.Models.FeeRuleCalculators
{
    public class FixedFeePerEntryRuleCalculator : RuleCalculatorBase
    {
        public decimal Fee { get; private set; }

        public FixedFeePerEntryRuleCalculator(TimeOnly startTime, TimeOnly endTime, decimal fee) : base(startTime, endTime)
        {
            this.Fee = fee;
        }

        public override decimal CalculateCost(FitResult fitResult)
        {
            if (!fitResult.IsFit)
            {
                return 0;
            }

            return this.Fee;
        }
    }
}