// <copyright file="TableStepTransformations.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Transformations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Table step transformations.
    /// </summary>
    [Binding]
    public static class TableStepTransformations
    {
        /// <summary>
        /// Transformation to produce a list of strings.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>A list of strings.</returns>
        [StepArgumentTransformation]
        public static IEnumerable<string> ListOfStringsTransformation(Table table)
        {
            Requires.NotNull(table, nameof(table));

            if (table.Rows.Count > 0 && table.Rows[0].Count > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(table), "Too many collumns");
            }

            var result = table.Rows.Select(r => r[0]);
            return result;
        }
    }
}
