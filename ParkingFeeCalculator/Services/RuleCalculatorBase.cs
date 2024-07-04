using System;
using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Utilities;
namespace ParkingFeeCalculator.Services
{
    public class RuleCalculatorBase
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public RuleCalculatorBase(TimeOnly startTime, TimeOnly endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public FitResult IsFit(DateTime startTime, DateTime endTime)
        {
            var actualStartTime = startTime.ToTimeOnly();
            var actualEndTime = endTime.ToTimeOnly();
            if ((actualStartTime < this.StartTime && actualEndTime < this.StartTime) ||
            (actualEndTime > this.EndTime && actualStartTime > this.EndTime))
            {
                return new FitResult { IsFit = false };
            }

            return new FitResult
            {
                IsFit = true,
                StartTime = actualStartTime > this.StartTime ? actualStartTime : this.StartTime,
                EndTime = actualEndTime < this.EndTime ? actualEndTime : this.EndTime
            };
        }

        public virtual decimal CalculateCost(FitResult fitResult)
        {
            throw new NotImplementedException();
        }
    }
}