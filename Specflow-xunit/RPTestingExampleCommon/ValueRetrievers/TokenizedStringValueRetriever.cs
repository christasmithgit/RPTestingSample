// <copyright file="TokenizedStringValueRetriever.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.ValueRetrievers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    /// <summary>
    /// A value retriever for strings to handle tokenized input
    /// </summary>
    public class TokenizedStringValueRetriever : IValueRetriever
    {
        private const string NullToken = "<null>";
        private static readonly Regex ControllerContextPropertyRegex = new Regex(@"\<ControllerContext\.(?<propertyName>[a-z0-9-_]*)\>", RegexOptions.IgnoreCase);
        private static readonly Regex ScenarioContextPropertyRegex = new Regex(@"\<ScenarioContext\[(?<keyName>[a-z0-9-_]*)\]\>", RegexOptions.IgnoreCase);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.String.</returns>
        public static string GetValue(string source)
        {
#pragma warning disable CS0618 // Type or member is obsolete - can't use context injection in a value retriever
            var context = ScenarioContext.Current;
#pragma warning restore CS0618 // Type or member is obsolete

            // Check for other defined patterns
            var result = source switch
            {
                var _ when string.Equals(source, NullToken, StringComparison.OrdinalIgnoreCase) => null,
                var _ when ScenarioContextPropertyRegex.IsMatch(source) => HandleScenarioContextProperty(source, context),
                _ => source,
            };

            return result;
        }

        /// <summary>
        /// Determines whether this instance can retrieve the specified key value pair.
        /// </summary>
        /// <param name="keyValuePair">The key value pair.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns><c>true</c> if this instance can retrieve the specified key value pair; otherwise, <c>false</c>.</returns>
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(string);
        }

        /// <summary>
        /// Retrieves the specified key value pair.
        /// </summary>
        /// <param name="keyValuePair">The key value pair.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>System.Object.</returns>
        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetValue(keyValuePair.Value);
        }
        private static string HandleScenarioContextProperty(string source, ScenarioContext context)
        {
            var keyName = ScenarioContextPropertyRegex.Match(source).Groups["keyName"].Value;
            var scenarioContext = context.ScenarioContainer.Resolve<ScenarioContext>();
            var propertyValue = scenarioContext[keyName]?.ToString();
            var result = ScenarioContextPropertyRegex.Replace(source, propertyValue);
            return result;
        }
    }
}
