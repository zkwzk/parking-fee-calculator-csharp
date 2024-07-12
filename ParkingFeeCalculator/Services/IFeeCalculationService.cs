using ParkingFeeCalculator.Models;

namespace ParkingFeeCalculator.Services
{
    public interface IFeeCalculationService
    {
        decimal CalculateParkingFee(DateTime startTime, DateTime endTime, VehicleType vehicleType, CarPark carPark);
    }
}