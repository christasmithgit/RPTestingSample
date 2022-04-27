// <copyright file="TokenizedStringParameter.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Helpers
{
    using RPTestingExample.TestCommon.ValueRetrievers;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Tokenized String Parameter - used in step definitions in place of a raw string to allow its tokens to be replaced
    /// </summary>
    [Binding]
    public class TokenizedStringParameter
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Transforms the passed string to a <see cref="TokenizedStringParameter"/>.
        /// </summary>
        /// <param name="value">The value to transform.</param>
        /// <returns>The <see cref="TokenizedStringParameter"/> resulting from the transformation.</returns>
        [StepArgumentTransformation]
        public static TokenizedStringParameter TokenizedStringParameterTransform(string value)
        {
            var actualValue = TokenizedStringValueRetriever.GetValue(value);
            return new TokenizedStringParameter
            {
                Value = actualValue,
            };
        }
    }
}
