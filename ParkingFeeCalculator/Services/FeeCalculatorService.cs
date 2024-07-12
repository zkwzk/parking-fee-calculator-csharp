using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Services
{
    public class FeeCalculatorService
    {
        public bool CheckIsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public bool CheckGracePeriod(DateTime startTime, DateTime endTime, int gracePeriodMinutes)
        {
            return (endTime - startTime).TotalMinutes <= gracePeriodMinutes;
        }

        public bool CheckIsSameDay(DateTime startTime, DateTime endTime)
        {
            return startTime.Date == endTime.Date;
        }

        public List<CalculateDaysResult> CalculateDays(DateTime startTime, DateTime endTime) 
        {
            var result = new List<CalculateDaysResult>();

            if (CheckIsSameDay(startTime, endTime))
            {
                result.Add(new CalculateDaysResult { DayStartTime = startTime.ToTimeOnly(), DayEndTime = endTime.ToTimeOnly(), IsWeekend = CheckIsWeekend(startTime) });
                return result;
            }

            var currentDay = startTime.Date;
            var currentStartTime = startTime.ToTimeOnly();
            var currentEndTime = new TimeOnly(23, 59, 0, 0);

            while (currentDay < endTime.Date)
            {
                result.Add(new CalculateDaysResult { DayStartTime = currentStartTime, DayEndTime = currentEndTime, IsWeekend = CheckIsWeekend(currentDay) });
                currentDay = currentDay.AddDays(1);
                currentStartTime = new TimeOnly(0, 0, 0, 0);
            }

            result.Add(new CalculateDaysResult { DayStartTime = new TimeOnly(0, 0, 0, 0), DayEndTime = endTime.ToTimeOnly(), IsWeekend = CheckIsWeekend(endTime) });
            return result;
        }

        public decimal CalculateParkingFee(DateTime startTime, DateTime endTime, VehicleType vehicleType, CarPark carPark)
        {
            if(CheckGracePeriod(startTime, endTime, carPark.GracePeriodInMinutes))
            {
                return 0;
            }

            var calculateDaysResult = CalculateDays(startTime, endTime);

            var result = 0m;
            foreach(var dayResult in calculateDaysResult)
            {
                if(vehicleType == VehicleType.Motorcycle)
                {
                    foreach(var ruleCalculator in carPark.MotorCycleFeeRuleCalculators)
                    {
                        var fitResult = ruleCalculator.IsFit(dayResult.DayStartTime, dayResult.DayEndTime);
                        if(fitResult.IsFit)
                        {
                            result += ruleCalculator.CalculateCost(fitResult);
                        }
                    }
                } 
                else {
                    var ruleCalculators = dayResult.IsWeekend ? carPark.CarWeekendFeeRuleCalculators : carPark.CarWeekdayFeeRuleCalculators;

                    foreach(var ruleCalculator in ruleCalculators)
                    {
                        var fitResult = ruleCalculator.IsFit(dayResult.DayStartTime, dayResult.DayEndTime);
                        if(fitResult.IsFit)
                        {
                            result += ruleCalculator.CalculateCost(fitResult);
                        }
                    }
                }
            }

            return result;
        }
    }
}