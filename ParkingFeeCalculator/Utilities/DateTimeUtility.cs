using System;
namespace ParkingFeeCalculator.Utilities
{
    public static class DateTimeUtility
    {
        public static TimeOnly ToTimeOnly(this DateTime dateTime)
        {
            return new TimeOnly(dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        }
    }
}