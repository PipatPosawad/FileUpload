using Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueueRepositories
{
    /// <summary>
    /// Repository for mail notification queue.
    /// </summary>
    public interface IMailNotificationQueueRepository
    {
        /// <summary>
        /// Sends the <paramref name="message"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(MailNotificationMessage message);
    }
}
