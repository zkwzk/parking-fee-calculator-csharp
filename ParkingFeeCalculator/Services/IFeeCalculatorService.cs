using ParkingFeeCalculator.Models;

namespace ParkingFeeCalculator.Services
{
    public interface IFeeCalculatorService
    {
        decimal CalculateParkingFee(DateTime entryTime, DateTime exitTime, VehicleType vehicleType, CarPark carPark);
    }
}