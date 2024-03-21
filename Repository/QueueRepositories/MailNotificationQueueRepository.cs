using Domain.Constants;
using Domain.Json;
using Domain.Messages;
using Domain.QueueRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.QueueRepositories
{
    /// <summary>
    /// Repository for mail notification queue.
    /// </summary>
    public class MailNotificationQueueRepository : BaseQueueRepository, IMailNotificationQueueRepository
    {
        protected override string QueueName => Queues.MailNotification;

        /// <summary>
        /// Initializes a <see cref="MailNotificationQueueRepository"/> instance
        /// </summary>
        /// <param name="configuration"></param>
        public MailNotificationQueueRepository(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Sends the <paramref name="message"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(MailNotificationMessage message)
        {
            ArgumentNullException.ThrowIfNull(message);

            var content = DefaultJsonSerializer.Serialize(message);
            await SendMessageAsync(content);
        }
    }
}
