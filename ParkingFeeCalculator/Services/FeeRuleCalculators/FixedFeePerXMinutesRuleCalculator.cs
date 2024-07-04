using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Services.FeeRuleCalculators
{
    public class FixedFeePerXMinutesRuleCalculator : RuleCalculatorBase
    {
        private readonly int _minutes;
        private readonly decimal _fee;

        public FixedFeePerXMinutesRuleCalculator(TimeOnly startTime, TimeOnly endTime, int minutes, decimal fee) : base(startTime, endTime)
        {
            _minutes = minutes;
            _fee = fee;
        }

        public override decimal CalculateCost(FitResult fitResult)
        {
            if (!fitResult.IsFit)
            {
                return 0;
            }

            if (fitResult.StartTime == fitResult.EndTime)
            {
                return _fee;
            }

            var duration = (fitResult.EndTime - fitResult.StartTime).TotalMinutes;
            var timeCeil = Math.Ceiling(duration / _minutes);
            return (_fee * (decimal)timeCeil).To2DecimalPlaces();
        }
    }
}