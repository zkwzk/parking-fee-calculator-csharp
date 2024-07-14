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
            var actualStartTime = startTime;
            var actualEndTime = endTime;
            if (actualStartTime < this.StartTime && actualEndTime < this.StartTime ||
                actualEndTime > this.EndTime && actualStartTime > this.EndTime)
            {
                return new FitResult(false);
            }

            return new FitResult(actualStartTime > this.StartTime ? actualStartTime : this.StartTime,
                                 actualEndTime < this.EndTime ? actualEndTime : this.EndTime,
                                 true);
        }

        public virtual decimal CalculateCost(FitResult fitResult)
        {
            throw new NotImplementedException();
        }
    }
}