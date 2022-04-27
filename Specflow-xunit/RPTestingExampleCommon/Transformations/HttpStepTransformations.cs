// <copyright file="HttpStepTransformations.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Transformations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Microsoft;
    using TechTalk.SpecFlow;

    /// <summary>
    /// HTTP related step transforms.
    /// </summary>
    [Binding]
    public static class HttpStepTransformations
    {
        /// <summary>
        /// Transformation to produce a Uri.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The Uri.</returns>
        [StepArgumentTransformation]
        public static Uri UriTransformation(string value)
        {
            return new Uri(value, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Transformation to produce a Uri.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>
        /// The Uri.
        /// </returns>
        [StepArgumentTransformation]
        public static IEnumerable<Uri> ListOfUrisTransformation(Table table)
        {
            Requires.NotNull(table, nameof(table));

            if (table.Rows.Count > 0 && table.Rows[0].Count > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(table), "Too many collumns");
            }

            var result = table.Rows.Select(r => new Uri(r[0], UriKind.RelativeOrAbsolute));
            return result;
        }

        /// <summary>
        /// Transformation to produce a <see cref="HttpMethod"/>.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The HttpMethod.</returns>
        [StepArgumentTransformation]
        public static HttpMethod HttpMethodTransformation(string method)
        {
            return new HttpMethod(method);
        }
    }
}
