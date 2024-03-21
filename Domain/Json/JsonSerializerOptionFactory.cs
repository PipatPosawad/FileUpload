using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Json
{
    /// <summary>
    /// Factory for various types of <see cref="JsonSerializerOptions"/>
    /// </summary>
    public static class JsonSerializerOptionFactory
    {
        private static JsonSerializerOptions _defaultOptions;

        /// <summary>
        /// Gets the default option used by the SDC Cloud application.
        /// The settings serialize in camel case and automatically turns strings into enums.
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerOptions GetDefaultOptions()
        {
            if (_defaultOptions != null)
            {
                return _defaultOptions;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };

            options.Converters.Add(new JsonStringEnumConverter());

            _defaultOptions = options;

            return options;
        }
    }
}
