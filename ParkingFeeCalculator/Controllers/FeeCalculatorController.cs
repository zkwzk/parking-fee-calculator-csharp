// generate a controller with the name FeeCalculatorController

using Microsoft.AspNetCore.Mvc;
using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Services;
using System;

namespace ParkingFeeCalculator.Controllers
{
    [Route("api/feeCalculator")]
    [ApiController]
    public class FeeCalculatorController : ControllerBase
    {
        private readonly IFeeCalculatorService _service;

        public FeeCalculatorController(IFeeCalculatorService service)
        {
            _service = service;
        }

        [HttpGet("getLowestCarpark")]
        public ActionResult<CarParkFee> GetLowestCarpark(DateTime startTime, DateTime endTime, VehicleType vehicleType)
        {
            List<CarParkFee> carParkFees = new List<CarParkFee>();
            foreach(var carPark in CarParkConfig.CarParks) {
                carParkFees.Add(new CarParkFee(carPark.Name, _service.CalculateParkingFee(startTime, endTime, vehicleType, carPark)));
            }

            return Ok(carParkFees.OrderBy(x => x.Fee).First());
        }
    }
}