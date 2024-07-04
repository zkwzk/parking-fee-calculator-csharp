namespace ParkingFeeCalculator.Utilities
{
    public static class DecimalUtility
    {
        public static decimal To2DecimalPlaces(this decimal value)
        {
            return decimal.Round(value, 2);
        }
    }
}