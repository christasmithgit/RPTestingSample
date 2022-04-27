// <copyright file="ControllerStepDefinitions.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft;
    using Newtonsoft.Json.Linq;
    using RPTestingExample.TestCommon.Contexts;
    using RPTestingExample.TestCommon.Helpers;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Step definitions for controller context.
    /// </summary>
    [Binding]
    public class ControllerStepDefinitions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerStepDefinitions"/> class.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        public ControllerStepDefinitions(ControllerContext controllerContext)
        {
            Requires.NotNull(controllerContext, nameof(controllerContext));

            this.ControllerContext = controllerContext;
        }

        /// <summary>
        /// Gets the controller context.
        /// </summary>
        /// <value>The controller context.</value>
        public ControllerContext ControllerContext { get; }

        /// <summary>
        /// Given I have no payload.
        /// </summary>
        [Given(@"I have no payload")]
        public void GivenIHaveNoPayload()
        {
            this.ControllerContext.Payload = null;
        }

        /// <summary>
        /// Given I have a request payload that is an empty JSON object.
        /// </summary>
        [Given(@"I have a request payload that is an empty JSON object")]
        public void GivenIHaveARequestPayloadThatIsAnEmptyJsonObject()
        {
            this.ControllerContext.Payload = "{}";
        }

        /// <summary>
        /// When I '{method}' the API '{uri}'.
        /// </summary>
        /// <param name="method">The request HTTP method.</param>
        /// <param name="uri">The request URI.</param>
        /// <returns>Task.</returns>
        [When(@"I '(.*)' the API '(.*)'")]
        public async Task WhenITheAPIAsync(HttpMethod method, string uri)
        {
            Requires.NotNull(uri, nameof(uri));

            this.ControllerContext.Request = new HttpRequestMessage(method, $"https://localhost:44324{uri}");
            if (this.ControllerContext.Payload != null)
            {
                this.ControllerContext.Request.Content = new StringContent(this.ControllerContext.Payload, Encoding.UTF8, "application/json");
            }

            this.ControllerContext.Response = await this.ControllerContext.TestClient.SendAsync(this.ControllerContext.Request).ConfigureAwait(false);
        }

        /// <summary>
        /// Then I receive HTTP status code '{expectedStatusCode}'.
        /// </summary>
        /// <param name="expectedStatusCode">The expected status code.</param>
        [Then(@"I receive HTTP status code '(.*)'")]
        public void ThenIReceiveHTTPStatusCode(HttpStatusCode expectedStatusCode)
        {
            this.ControllerContext.Response.StatusCode
                .Should().Be(expectedStatusCode);
        }

        /// <summary>
        /// Then the response contains a json object.
        /// </summary>
        /// <returns>Task.</returns>
        [Then(@"the response contains a JSON Object")]
        public async Task ThenTheResponseContainsAJsonObjectAsync()
        {
            var responseContent = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var jsonResponseContent = JToken.Parse(responseContent);
            jsonResponseContent.Should().NotBeNull();
        }

        /// <summary>
        /// Then the response is an array containing '{expectedItemCount}' items.
        /// </summary>
        /// <param name="expectedItemCount">The expected item count.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Then(@"the response is an array containing '(.*)' items")]
        public async Task ThenTheResponseIsAnArrayContainingItemsAsync(int expectedItemCount)
        {
            var stringContent = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var content = JsonSerializer.Deserialize<IEnumerable<object>>(stringContent, JsonSerializationOptionDefaults.Test);
            content.Should().NotBeNull();
            content.Should().HaveCount(expectedItemCount);
        }

        /// <summary>
        /// Then the response contains no content.
        /// </summary>
        /// <returns>Task.</returns>
        [Then(@"the response contains no content")]
        public async Task ThenTheResponseContainsNoContentAsync()
        {
            var responseContent = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            responseContent.Should().BeNullOrEmpty();
        }

        /// <summary>
        /// Then the response contains '{expected}'.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <returns>A task that completes when the step is completed.</returns>
        [Then(@"the response contains '(.*)'")]
        public async Task ThenTheResponseContainsAsync(string expected)
        {
            var payload = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            payload.Should().Contain(expected);
        }

        /// <summary>
        /// Then the response contains the following '{expectedValues}':
        /// </summary>
        /// <param name="expectedValues">The expected values.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Then(@"the response contains the following:")]
        public async Task ThenTheResponseContainsTheFollowingAsync(Table expectedValues)
        {
            Requires.NotNull(expectedValues, nameof(expectedValues));

            var content = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var json = JObject.Parse(content);

            foreach (var row in expectedValues.Rows)
            {
                var path = row["Path"];
                var value = TokenizedStringParameter.TokenizedStringParameterTransform(row["Value"]).Value;
                json.SelectToken(path).Should().NotBeNull("{0} should be present in the response", path);
                json.SelectToken(path).Value<string>().Should().Be(value);
            }
        }

        /// <summary>
        /// Then the response result contains '{expectedNumberOfResults}' results.
        /// </summary>
        /// <param name="expectedNumberOfResults">The expected number of results.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Then(@"the response result contains '(.*)' result\(s\)")]
        public async Task ThenTheResponseResultContiansResultS(int expectedNumberOfResults)
        {
            var content = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var json = JObject.Parse(content);
            json.SelectToken("result").SelectToken("results").Should().HaveCount(expectedNumberOfResults);
        }

        /// <summary>
        /// Then the response contains the following substrings.
        /// </summary>
        /// <param name="table">The table of paths and expected values.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Then(@"the response contains the following substrings:")]
        public async Task ThenTheResponseContainsTheFollowingSubstringsAsync(Table table)
        {
            Requires.NotNull(table, nameof(table));

            var content = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var json = JObject.Parse(content);

            foreach (var row in table.Rows)
            {
                var path = row["Path"];
                var value = TokenizedStringParameter.TokenizedStringParameterTransform(row["Value"]).Value;
                json.SelectToken(path).Should().NotBeNull("{0} should be present in the response", path);
                json.SelectToken(path).Value<string>().Should().Contain(value);
            }
        }

        /// <summary>
        /// Then the response does not contain the following.
        /// </summary>
        /// <param name="table">The table of paths.</param>
        /// <returns>A task that completes when the step has completed.</returns>
        [Then(@"the response does not contain the following:")]
        public async Task ThenTheResponseDoesNotContainTheFollowingAsync(Table table)
        {
            Requires.NotNull(table, nameof(table));

            var content = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var json = JObject.Parse(content);

            foreach (var row in table.Rows)
            {
                json.SelectToken(row["Path"]).Should().BeNull("{0} should not be present in the response", row["Path"]);
            }
        }

        /// <summary>
        /// Then I output the request payload to the console.
        /// </summary>
        [Then(@"I output the request payload to the console")]
        public void ThenIOutputTheRequestPayloadToTheConsole()
        {
            Console.WriteLine(this.ControllerContext.Payload);
        }

        /// <summary>
        /// Then I output the response content to the console.
        /// </summary>
        /// <returns>Task.</returns>
        [Then(@"I output the response content to the console")]
        public async Task ThenIOutputTheResponseToTheConsoleAsync()
        {
            var content = await this.ControllerContext.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Console.WriteLine(content);
        }
    }
}
