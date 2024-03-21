using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    /// <summary>
    /// Contains keys for temporary blob storage before archive
    /// </summary>
    public static class BlobStorageMetadataKeys
    {
        /// <summary>
        /// Original file name
        /// </summary>
        public const string OriginalFileName = "OriginalFileName";

        /// <summary>
        /// Original file size in bytes
        /// </summary>
        public const string OriginalFileSize = "OriginalFileSize";

        /// <summary>
        /// Original content type
        /// </summary>
        public const string OriginalContentType = "OriginalContentType";
    }
}
