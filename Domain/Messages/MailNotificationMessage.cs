using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Messages
{
    /// <summary>
    /// A message to send mail notification
    /// </summary>
    public class MailNotificationMessage
    {
        /// <summary>
        /// Document Identifier
        /// </summary>
        public Guid FileId { get; init; }
    }
}
