using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Utilities;

namespace ParkingFeeCalculator.Services
{
    public class FeeCalculationService : IFeeCalculationService
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
                result.Add(new CalculateDaysResult(startTime.ToTimeOnly(), endTime.ToTimeOnly(), CheckIsWeekend(startTime)));
                return result;
            }

            var currentDay = startTime.Date;
            var currentStartTime = startTime.ToTimeOnly();
            var currentEndTime = new TimeOnly(23, 59);

            while (currentDay < endTime.Date)
            {
                result.Add(new CalculateDaysResult(currentStartTime, currentEndTime, CheckIsWeekend(currentDay)));
                currentDay = currentDay.AddDays(1);
                currentStartTime = new TimeOnly(0, 0);
            }

            result.Add(new CalculateDaysResult(currentStartTime, endTime.ToTimeOnly(), CheckIsWeekend(endTime)));
            return result;
        }

        public decimal CalculateParkingFee(DateTime startTime, DateTime endTime, VehicleType vehicleType, CarPark carPark)
        {
            decimal result = 0m;
            //#task 3
            //TODO: Implement the logic to calculate the parking fee
            return result.ToTwoDecimalPlaces();
        }
    }
}