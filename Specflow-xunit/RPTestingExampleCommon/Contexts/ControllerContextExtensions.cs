// <copyright file="ControllerContextExtensions.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Contexts
{
    using System;
    using System.Net.Http;
    using Microsoft;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Extensions for the <see cref="ControllerContext"/> class.
    /// </summary>
    public static class ControllerContextExtensions
    {
        /// <summary>
        /// Creates the in memory server and client for controller tests.
        /// </summary>
        /// <typeparam name="TStartup">The startup class for the server.</typeparam>
        /// <param name="controllerContext">The controller context.</param>
        public static void CreateInMemoryServerAndClient<TStartup>(this ControllerContext controllerContext)
            where TStartup : class
        {
            controllerContext.CreateInMemoryServerAndClient<TStartup>(null);
        }

        /// <summary>
        /// Creates the in memory server and client for controller tests.
        /// </summary>
        /// <typeparam name="TStartup">The startup class for the server.</typeparam>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="configure">Action to add configuration providers to the server.</param>
        /// <param name="configureServices">The configure services.</param>
        public static void CreateInMemoryServerAndClient<TStartup>(this ControllerContext controllerContext, Action<IConfigurationBuilder> configure, Action<WebHostBuilderContext, IServiceCollection> configureServices = null)
            where TStartup : class
        {
            TestServer tempServer = null;
            HttpClient tempClient = null;

            Requires.NotNull(controllerContext, nameof(controllerContext));

            try
            {
                var builder = new WebHostBuilder();
                if (configure != null)
                {
                    builder.ConfigureAppConfiguration(configure);
                }

                builder.UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = true;
                    options.ValidateOnBuild = true;
                });
                builder.UseStartup<TStartup>();

                if (configureServices != null)
                {
                    builder.ConfigureServices(configureServices);
                }

                tempServer = new TestServer(builder);
                tempClient = tempServer.CreateClient();
                controllerContext.TestServer = tempServer;
                controllerContext.TestClient = tempClient;
                tempServer = null;
                tempClient = null;
            }
            finally
            {
                if (tempServer != null)
                {
                    tempServer.Dispose();
                }

                if (tempClient != null)
                {
                    tempClient.Dispose();
                }
            }
        }
    }
}
