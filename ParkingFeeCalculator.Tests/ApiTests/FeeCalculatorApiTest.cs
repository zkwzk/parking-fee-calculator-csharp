using System.Net;
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
        public async Task ShouldReturnOKStatusWithCorrectResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/feecalculator/getLowestCarpark?startTime=2021-01-01T10%3A00%3A00.000Z&endTime=2021-01-01T11%3A00%3A00.000Z&vehicleType=0");
            var expectedResponse = "{\"name\":\"Plaza Singapura Car Park\",\"fee\":1.95}";
            var responseString = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResponse, responseString);
        }
    }
}