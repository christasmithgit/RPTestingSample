// <copyright file="ControllerContext.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Contexts
{
    using Microsoft.AspNetCore.TestHost;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;

    /// <summary>
    /// The context for any controller tests
    /// </summary>
    public class ControllerContext : IDisposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="ControllerContext"/> class.
        /// </summary>
        [ExcludeFromCodeCoverage]
        ~ControllerContext()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets or sets the test server.
        /// </summary>
        public TestServer TestServer { get; set; }

        /// <summary>
        /// Gets or sets the test client.
        /// </summary>
        public HttpClient TestClient { get; set; }

        /// <summary>
        /// Gets or sets the payload to be included in a request body.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Gets or sets the response from the last call to the controller.
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// Gets or sets the request from the last call to the controller.
        /// </summary>
        public HttpRequestMessage Request { get; set; }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Request?.Dispose();
                this.Response?.Dispose();
                this.TestClient?.Dispose();
                this.TestServer?.Dispose();
            }
        }
    }
}
