using Newtonsoft.Json;
using RPTestingExample_PlainXunit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RPTestingExample_Plain.Integration.ControllerTests
{
    public class WeatherControllerTest : BaseControllerTest
    {
        public WeatherControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsAllRecords()
        {
            var client = this.GetNewClient();
            var response = await client.GetAsync("/WeatherForecast");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(stringResponse).ToList();
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);
            Assert.True(result.Count == 5);
        }
    }
}
