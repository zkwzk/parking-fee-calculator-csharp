namespace ParkingFeeCalculator.Models
{
    public class CarParkFee
    {
        public String Name { get; private set; }
        public decimal Fee { get; private set; }

        public CarParkFee(String name, decimal fee)
        {
            this.Name = name;
            this.Fee = fee;
        }
    }
} 