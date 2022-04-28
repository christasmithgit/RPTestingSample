using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RPTestingExample_PlainXunit.Interfaces;
using System;
using System.Linq;

namespace RPTestingExample_Plain.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        public void CustomConfigureServices(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStorageService, MockStorageService>();

            });
        }

    }
}