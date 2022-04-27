// <copyright file="JsonSerializationOptionDefaults.cs" company="Microsoft Services">
// Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace RPTestingExample.TestCommon.Helpers
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// JSON Serialization Option defaults.
    /// </summary>
    public static class JsonSerializationOptionDefaults
    {
        /// <summary>
        /// Gets the default JSON Serializer options to use in Test Projects.  These work with the ASP.NET Core defaults.
        /// </summary>
        /// <value>The default JSON Serializer options.</value>
        /// <remarks>
        /// The <see cref="JsonStringEnumConverter"/> is included to ensure that enums are serialized to strings.
        /// <see cref="JsonSerializerOptions.WriteIndented"/> is set to <c>true</c> to help with testing.
        /// <see cref="JsonSerializerOptions.PropertyNamingPolicy"/> is set to the default <see cref="JsonNamingPolicy.CamelCase"/>.
        /// <see cref="JsonSerializerOptions.PropertyNameCaseInsensitive"/> is left as the default <c>false</c>, this makes tests and code case-sensitive.
        /// </remarks>
        public static readonly JsonSerializerOptions Test = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        /// <summary>
        /// The ASP.NET default JSON Serializer options
        /// </summary>
        public static readonly JsonSerializerOptions AspNet = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }
}
