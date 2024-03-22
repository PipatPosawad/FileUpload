using Domain;
using Domain.BlobRepositories;
using Domain.Constants;
using Domain.Services;
using Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MailNotificationService : IMailNotificationService
    {
        private readonly ILogger _logger;
        private readonly IFileBlobRepository _fileBlobRepository;
        private readonly ISmtpClientFactory _smtpClientFactory;

        private MailNotificationSettings _mailNotificationSettings { get; }

        public MailNotificationService(IFileBlobRepository fileBlobRepository,
            IOptions<MailNotificationSettings> mailNotificationSettings,
            ILogger logger,
            ISmtpClientFactory smtpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileBlobRepository = fileBlobRepository ?? throw new ArgumentNullException(nameof(fileBlobRepository));
            _smtpClientFactory = smtpClientFactory ?? throw new ArgumentNullException(nameof(smtpClientFactory));

            if (mailNotificationSettings?.Value == null)
            {
                throw new ArgumentNullException(nameof(mailNotificationSettings));
            }

            _mailNotificationSettings = mailNotificationSettings.Value;
        }

        public async Task SendMailAsync(Guid fileId)
        {
            var metadata = await _fileBlobRepository.GetFileMetadataAsync(fileId);
            if (!metadata.TryGetValue(BlobStorageMetadataKeys.OriginalFileName, out var originalFileName))
            {
                throw new Exception("Cannot get file name from metadata.");
            }
            
            string body = $"This is a test email sent from {originalFileName}.";

            // Create the email message
            var mail = new MailMessage(_mailNotificationSettings.FromAddress, _mailNotificationSettings.ToAddress, _mailNotificationSettings.Subject, body);

            // Create the SMTP client
            var smtpClient = _smtpClientFactory.Create(_mailNotificationSettings.SmtpServer, _mailNotificationSettings.SmtpPort);
#if DEBUG
            smtpClient.EnableSsl = false;
#endif

            try
            {
                // Send the email
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send email: " + ex.Message);
                throw;
            }
            finally
            {
                // Dispose of objects
                mail.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
