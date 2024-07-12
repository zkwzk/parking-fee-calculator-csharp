namespace ParkingFeeCalculator.Models.FeeRuleCalculators
{
    public class RuleCalculatorBase
    {
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }

        public RuleCalculatorBase(TimeOnly startTime, TimeOnly endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public FitResult IsFit(TimeOnly startTime, TimeOnly endTime)
        {
            // task #2
            // TODO: Implement the isFit method
            return new FitResult(false);
        }

        public virtual decimal CalculateCost(FitResult fitResult)
        {
            throw new NotImplementedException();
        }
    }
}