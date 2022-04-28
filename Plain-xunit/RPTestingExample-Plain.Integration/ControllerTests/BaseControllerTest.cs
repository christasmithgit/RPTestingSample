using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace RPTestingExample_Plain.Integration.ControllerTests
{
    public class BaseControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public BaseControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public HttpClient GetNewClient()
        {
            var newClient = _factory.WithWebHostBuilder(builder =>
            {
                _factory.CustomConfigureServices(builder);
            }).CreateClient();

            return newClient;
        }
    }
}