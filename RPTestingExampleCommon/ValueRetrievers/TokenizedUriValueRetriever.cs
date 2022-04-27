// <copyright file="TokenizedUriValueRetriever.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.ValueRetrievers
{
    using System;
    using System.Collections.Generic;
    using TechTalk.SpecFlow.Assist;

    /// <summary>
    /// A value retriever for URIs to handle tokenized input
    /// Implements the <see cref="IValueRetriever" />
    /// </summary>
    /// <seealso cref="IValueRetriever" />
    public class TokenizedUriValueRetriever : IValueRetriever
    {
        /// <summary>
        /// Determines if this retriever can retrieve the actual value from a key-&gt;value set in a table.
        /// </summary>
        /// <param name="keyValuePair">Key value pair.</param>
        /// <param name="targetType">The type of the object that is being built from the table.</param>
        /// <param name="propertyType">The type of the property or member that is being set.</param>
        /// <returns><c>true</c> if this instance can retrieve the specified key-&gt;value; otherwise, <c>false</c>.</returns>
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(Uri);
        }

        /// <summary>
        /// Retrieve the value from a key-&gt; value set, as the expected type on targetType.
        /// </summary>
        /// <param name="keyValuePair">Key value pair.</param>
        /// <param name="targetType">The type of the object that is being built from the table.</param>
        /// <param name="propertyType">The type of the property or member that is being set.</param>
        /// <returns>System.Object.</returns>
        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var parsedValue = TokenizedStringValueRetriever.GetValue(keyValuePair.Value);
            return string.IsNullOrEmpty(parsedValue) ? null : new Uri(parsedValue, UriKind.RelativeOrAbsolute);
        }
    }
}
