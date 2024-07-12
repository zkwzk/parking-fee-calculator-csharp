using ParkingFeeCalculator.Models;
using ParkingFeeCalculator.Services;

namespace ParkingFeeCalculator.Tests.Services
{
    public class FeeCalculationServiceTest
    {
        private FeeCalculationService _service = new FeeCalculationService();

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
        public void ShouldReturnTrueIfItsSameDay()
        {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 12, 10, 10, 0);
            var result = _service.CheckIsSameDay(startTime, endTime);
            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseIfNotSameDay()
        {
            var startTime = new DateTime(2024, 7, 12, 23, 50, 0);
            var endTime = new DateTime(2024, 7, 13, 0, 0, 0);
            var result = _service.CheckIsSameDay(startTime, endTime);
            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTwoItemsInTheResultForCalculateDaysIfItsAcrossDays()
        {
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
        public void ShouldReturnOneItemInTheResultForCalculateDaysIfItsSameDay()
        {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 12, 16, 0, 0);
            var result = _service.CalculateDays(startTime, endTime);
            Assert.Single(result);
            Assert.Equal(new TimeOnly(10, 0), result[0].DayStartTime);
            Assert.Equal(new TimeOnly(16, 0), result[0].DayEndTime);
            Assert.False(result[0].IsWeekend);
        }

        [Fact]
        public void ShouldReturnZeroIfItsWithinGracePeriod()
        {
            var startTime = new DateTime(2024, 7, 12, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 12, 10, 10, 0);
            var carPark = CarParkConfig.PlazaSingapuraCarPark;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, carPark);
            Assert.Equal(0m, result);
        }

        [Fact]
        public void ShouldReturnCorrectFeeForCalculateParkingFeeIfItsSameDay()
        {
            var startTime = new DateTime(2021, 1, 1, 10, 0, 0);
            var endTime = new DateTime(2021, 1, 1, 11, 0, 0);
            var carPark = CarParkConfig.PlazaSingapuraCarPark;
            var result = _service.CalculateParkingFee(startTime, endTime, VehicleType.Car, carPark);
            Assert.Equal(1.95m, result);
        }

        // task #3
        // TODO: implement more test cases for the CalculateParkingFee method
    }
}