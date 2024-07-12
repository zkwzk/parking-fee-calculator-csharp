using ParkingFeeCalculator.Models;

namespace ParkingFeeCalculator.Services
{
    public interface IFeeCalculationService
    {
        decimal CalculateParkingFee(DateTime entryTime, DateTime exitTime, VehicleType vehicleType, CarPark carPark);
    }
}