using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Json
{
    /// <summary>
    /// A JSON serializer using the default options. See <see cref="JsonSerializerOptionFactory.GetDefaultOptions"/>
    /// </summary>
    public static class DefaultJsonSerializer
    {
        /// <summary>
        /// Serializes an object
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize<TValue>(TValue value) => JsonSerializer.Serialize(value, JsonSerializerOptionFactory.GetDefaultOptions());

        /// <summary>
        /// Deserializes a JSON string to a given <typeparamref name="TType"/>. 
        /// Throws if the input JSON string is invalid.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public static TType Deserialize<TType>(string jsonString) => JsonSerializer.Deserialize<TType>(jsonString, JsonSerializerOptionFactory.GetDefaultOptions());
    }
}
