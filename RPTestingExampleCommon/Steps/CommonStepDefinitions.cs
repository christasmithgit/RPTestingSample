// <copyright file="CommonStepDefinitions.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Steps
{
    using System;
    using Microsoft;
    using Microsoft.Extensions.DependencyInjection;
    using RPTestingExample.Interfaces;
    using RPTestingExample;
    using RPTestingExample.TestCommon.Contexts;
    using TechTalk.SpecFlow;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Common Step Definitions.
    /// </summary>
    [Binding]
    public class CommonStepDefinitions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonStepDefinitions" /> class.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="testKitContext">The test kit context.</param>
        /// <param name="referenceContext">The reference context.</param>
        public CommonStepDefinitions(ControllerContext controllerContext)
        {
            Requires.NotNull(controllerContext, nameof(controllerContext));

            this.ControllerContext = controllerContext;
        }

        /// <summary>
        /// Gets the controller context.
        /// </summary>
        /// <value>The controller context.</value>
        public ControllerContext ControllerContext { get; }

        [Given(@"I am running the KX Resource Provider in a test server")]
        public void GivenIAmRunningTheKXResourceProviderInATestServer()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            // Set up the test client and server
            this.ControllerContext.CreateInMemoryServerAndClient<Startup>(
            builder =>
            {
                // Use test app settings
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                builder.AddJsonFile("testsettings.json", optional: false, reloadOnChange: true);
                builder.AddJsonFile($"testsettings.{environment}.json", optional: true, reloadOnChange: true);
            },
            (context, services) =>
            {
            });

            stopwatch.Stop();
            Console.WriteLine($"Create Test Server time: {stopwatch.ElapsedMilliseconds} ms");
        }

    }
}
