namespace ParkingFeeCalculator.Utilities
{
    public static class DecimalUtility
    {
        public static decimal ToTwoDecimalPlaces(this decimal value)
        {
            return decimal.Round(value, 2);
        }
    }
}