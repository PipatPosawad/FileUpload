using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    /// <summary>
    /// Represents document content
    /// </summary>
    public class FileContentModel
    {
        /// <summary>
        /// File name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File stream
        /// </summary>
        public Stream FileStream { get; set; }

        /// <summary>
        /// Content type
        /// </summary>
        public string ContentType { get; set; }
    }
}
