using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.QueueRepositories
{
    /// <summary>
    /// Repository for <see cref="QueueClient"/>
    /// </summary>
    public abstract class BaseQueueRepository
    {
        /// <summary>
        /// Queue name
        /// </summary>
        protected abstract string QueueName { get; }

        private readonly IConfiguration _configuration;
        private const string ConnectionStringKey = "SystemStorage";

        /// <summary>
        /// Initializes an <see cref="BaseQueueRepository"/> instance
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected BaseQueueRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Sends the <paramref name="message"/> to the queue
        /// </summary>
        /// <param name="message">A message which is send to the queue.</param> 
        /// <returns></returns>
        protected async Task SendMessageAsync(string message)
        {
            var client = await CreateQueueClientAsync(QueueName);
            var result = await client.SendMessageAsync(message);
            if (result.GetRawResponse().Status != StatusCodes.Status201Created)
            {
                throw new Exception("An error occurred while sending the message to the queue");
            }
        }

        private async Task<QueueClient> CreateQueueClientAsync(string queueName)
        {
            var connectionString = _configuration.GetConnectionString(ConnectionStringKey);
            var queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });

            // Create queue if not exists
            // Note that no need to create the poison queue
            await queueClient.CreateIfNotExistsAsync();

            return queueClient;
        }
    }
}
