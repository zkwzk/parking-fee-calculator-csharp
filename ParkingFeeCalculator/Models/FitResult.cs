namespace ParkingFeeCalculator.Models
{
    public class FitResult
    {
        public bool IsFit { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }

        public FitResult(TimeOnly startTime, TimeOnly endTime, bool isFit)
        {
            this.IsFit = isFit;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public FitResult(bool isFit)
        {
            this.IsFit = isFit;
        }
    }
}