// <copyright file="TokenizedUriParameter.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Helpers
{
    using System;
    using RPTestingExample.TestCommon.ValueRetrievers;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Tokenized Uri Parameter - used in step definitions in place of a raw Uri to allow its tokens to be replaced
    /// </summary>
    [Binding]
    public class TokenizedUriParameter
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public Uri Value { get; set; }

        /// <summary>
        /// Transforms the passed string to a <see cref="TokenizedUriParameter"/>.
        /// </summary>
        /// <param name="value">The value to transform.</param>
        /// <returns>The <see cref="TokenizedUriParameter"/> resulting from the transformation.</returns>
        [StepArgumentTransformation]
        public static TokenizedUriParameter TokenizedUriParameterTransform(string value)
        {
            var actualValue = TokenizedStringValueRetriever.GetValue(value);
            return new TokenizedUriParameter
            {
                Value = new Uri(actualValue, UriKind.RelativeOrAbsolute),
            };
        }
    }
}
