using System;

namespace ParkingFeeCalculator.Models
{
    public class FitResult
    {
        public bool IsFit { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}