using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Contains helper methods for Uri
    /// </summary>
    public static class UriHelper
    {
        private const string KeyVaultResource = ".vault.azure.net";

        /// <summary>
        /// Checks whether the given Uri is Azure KeyVault resource.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool IsKeyVault(Uri uri)
        {
            var host = uri.Host;
            return host.EndsWith(KeyVaultResource, StringComparison.OrdinalIgnoreCase);
        }
    }
}
