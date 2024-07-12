using Microsoft.AspNetCore.Mvc.Testing;

namespace ParkingFeeCalculator.Tests.ApiTests
{
    public class FeeCalculatorApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public FeeCalculatorApiTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturn200WithCorrectResponse() {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/feecalculator/getLowestCarpark?startTime=2021-01-01T10%3A00%3A00.000Z&endTime=2021-01-01T11%3A00%3A00.000Z&vehicleType=0");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"name\":\"Plaza Singapura Car Park\",\"fee\":1.95}", responseString);
        }
    }
}   