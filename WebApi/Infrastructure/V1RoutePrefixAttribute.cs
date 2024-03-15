using Microsoft.AspNetCore.Mvc;

namespace WebApi.Infrastructure
{
    /// <summary>
    /// Specifies v1 prefix attribute route/
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class V1RoutePrefixAttribute : RouteAttribute
    {
        /// <summary>
        /// The prefix
        /// </summary>
        public const string V1Prefix = "api/v1";

        /// <summary>
        /// Initializes a new <see cref="V1RoutePrefixAttribute"/> instance.
        /// </summary>
        /// <param name="prefix">The v1 prefix.</param>
        /// <param name="additionalprefixes">The additional prefixes.</param>
        public V1RoutePrefixAttribute(string prefix, params string[] additionalprefixes)
            : base(GetRouteTemplate(prefix, additionalprefixes))
        {
        }

        private static string GetRouteTemplate(string prefix, params string[] additionalprefixes)
        {
            var routeTemplate = FormattableString.Invariant($"{V1Prefix}/{prefix}");
            if (additionalprefixes != null && additionalprefixes.Length > 0)
                routeTemplate += string.Join("/", additionalprefixes);

            return routeTemplate;
        }
    }
}
