using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Services;

namespace ParkingFeeCalculator.Tests.Services
{
    public class FeeCalculatorServiceTest
    {
        private FeeCalculatorService _service  = new FeeCalculatorService();

        [Fact]
        public void ShouldReturnTrueIfItsSaturday()
        {
            var date = new DateTime(2024, 7, 13); // Saturday
            var result = _service.CheckIsWeekend(date);
            Assert.True(result);

        }

        [Fact]  
        public void ShouldReturnTrueIfItsSunday()
        {
            var date = new DateTime(2024, 7, 14); // Sunday
            var result = _service.CheckIsWeekend(date);
            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseIfItsFriday()
        {
            var date = new DateTime(2024, 7, 12); // Friday
            var result = _service.CheckIsWeekend(date);
            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTrueIfWithinGracePeriod()
        {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 12, 10, 10, 0);

            var result = _service.CheckGracePeriod(startTime, endTime, 10);
            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseIfExceedsGracePeriod()
        {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 12, 10, 11, 0);

            var result = _service.CheckGracePeriod(startTime, endTime, 10);
            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTrueIfAcrossDayAndWithinGracePeriod()
        {
            var startTime = new DateTime(2024, 7, 12, 23, 50, 0);
            var endTime = new DateTime(2024, 7, 13, 0, 0, 0);

            var result = _service.CheckGracePeriod(startTime, endTime, 10);
            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseIfAcrossDayAndExceedsGracePeriod()
        {
            var startTime = new DateTime(2024, 7, 12, 23, 50, 0);
            var endTime = new DateTime(2024, 7, 13, 0, 1, 0);

            var result = _service.CheckGracePeriod(startTime, endTime, 10);
            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTrueIfItsSameDay() {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 12, 10, 10, 0);
            var result = _service.CheckIsSameDay(startTime, endTime);
            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseIfNotSameDay() {
            var startTime = new DateTime(2024, 7, 12, 23, 50, 0);
            var endTime = new DateTime(2024, 7, 13, 0, 0, 0);
            var result = _service.CheckIsSameDay(startTime, endTime);
            Assert.False(result);
        }

        [Fact]
        public void ShouldReturn2ItemsInTheResultForCalculateDaysIfItsAcrossDays() {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 13, 10, 0, 0);
            var result = _service.CalculateDays(startTime, endTime);
            Assert.Equal(2, result.Count);
            Assert.Equal(new TimeOnly(10, 0), result[0].DayStartTime);
            Assert.Equal(new TimeOnly(23, 59), result[0].DayEndTime);
            Assert.False(result[0].IsWeekend);
            Assert.Equal(new TimeOnly(0, 0), result[1].DayStartTime);
            Assert.Equal(new TimeOnly(10, 0), result[1].DayEndTime);
            Assert.True(result[1].IsWeekend);
        }

         [Fact]
        public void ShouldReturn1ItemsInTheResultForCalculateDaysIfItsSameDay() {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 12, 16, 0, 0);
            var result = _service.CalculateDays(startTime, endTime);
            Assert.Single(result);
            Assert.Equal(new TimeOnly(10, 0), result[0].DayStartTime);
            Assert.Equal(new TimeOnly(16, 00), result[0].DayEndTime);
            Assert.False(result[0].IsWeekend);
        }

        [Fact]
        public void ShouldReturn0IfItsWithinGracePeriod() {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 12, 10, 10, 0);
            var carPark = CarParkConfig.PlazaSingapuraCarPark;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, carPark);
            Assert.Equal(0, result);
        }

        [Fact]
        public void ShouldReturnCorrectFeeForCalculateParkingFeeIfItsSameDay() {
            var startTime = new DateTime(2021, 1, 1, 10, 0, 0);
            var endTime = new DateTime(2021, 1, 1, 11, 0, 0);
            var carPark = CarParkConfig.PlazaSingapuraCarPark;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, carPark);
            Assert.Equal(1.95m, result);
        }

        [Fact]
        public void ShouldReturnCorrectFeeForCalculateParkingFeeIfIts2Hours() {
            var startTime = new DateTime(2021, 1, 1, 10, 0, 0);
            var endTime = new DateTime(2021, 1, 1, 12, 0, 0);
            var carPark = CarParkConfig.PlazaSingapuraCarPark;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, carPark);
            Assert.Equal(4.15m, result);
        }

        [Fact]
        public void ShouldReturnCorrectFeeForCalculateParkingFeeIfItsOneWeekdayAndOneWeekend() {
            /*
                10-1100: 1.95
                1100-17:59: 7*4*0.55 = 15.40
                18-23:59: 3.25

                12-0259: 3*4*0.55 = 6.60
                3-500: 3.25
                501-1100: 6*4*0.55 = 13.2

                total: 1.95 + 15.40 + 3.25 + 6.60 + 3.25 + 13.2 = 43.65
            */
            var startTime = new DateTime(2021, 1, 1, 10, 0, 0);
            var endTime = new DateTime(2021, 1, 2, 11, 0, 0);
            var carPark = CarParkConfig.PlazaSingapuraCarPark;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, carPark);
            Assert.Equal(43.65m, result);
        }

        [Fact]
        public void ShouldReturnCorrectFeeForCalculateParkingFeeIfItsOneWeekdayAndTwoWeekend() {
            var startTime = new DateTime(2021, 1, 1, 10, 0, 0);
            var endTime = new DateTime(2021, 1, 3, 12, 0, 0);
            var carPark = CarParkConfig.PlazaSingapuraCarPark;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, carPark);
            Assert.Equal(87.55m, result);
        }

        [Fact]
        public void ShouldReturnCorrectFeeForCalculateParkingFeeForMotorcycle() {
            var startTime = new DateTime(2021, 1, 1, 10, 0, 0);
            var endTime = new DateTime(2021, 1, 1, 12, 0, 0);
            var carPark = CarParkConfig.PlazaSingapuraCarPark;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Motorcycle, carPark);
            Assert.Equal(1.3m, result);
        }

        [Fact]
        public void PlazaSingapuraShouldBeTheLowest() {
            var startTime = new DateTime(2021, 1, 1, 0, 0, 0);
            var endTime = new DateTime(2021, 1, 1, 23, 59, 0);
            var expectedFeeForPlazaSingapura = 42.6m;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, CarParkConfig.PlazaSingapuraCarPark);
            Assert.Equal(expectedFeeForPlazaSingapura, result);

            var expectedFeeForOrchardCentral = 53.06m;
            result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car,  CarParkConfig.OrchardCentralCarPark);
            Assert.Equal(expectedFeeForOrchardCentral, result);

            var expecteFeeForTSC = 58.46m;
            result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, CarParkConfig.TSCCarPark);
            Assert.Equal(expecteFeeForTSC, result);
            Assert.True(expectedFeeForPlazaSingapura < expectedFeeForOrchardCentral);
            Assert.True(expectedFeeForPlazaSingapura < expecteFeeForTSC);
        }
    }
}