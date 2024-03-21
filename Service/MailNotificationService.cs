using Domain.BlobRepositories;
using Domain.Constants;
using Domain.Services;
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
        private readonly IFileBlobRepository _fileBlobRepository;

        public MailNotificationService(IFileBlobRepository fileBlobRepository)
        {
            _fileBlobRepository = fileBlobRepository ?? throw new ArgumentNullException(nameof(fileBlobRepository));
        }

        public async Task SendMailAsync(Guid fileId)
        {
            var metadata = await _fileBlobRepository.GetFileMetadataAsync(fileId);
            if (!metadata.TryGetValue(BlobStorageMetadataKeys.OriginalFileName, out var originalFileName))
            {
                throw new Exception("Cannot get file name from metadata.");
            }

            string smtpServer = "127.0.0.1"; // Papercut SMTP server address
            int smtpPort = 25; // Papercut SMTP server port
            string fromAddress = "sender@example.com";
            string toAddress = "recipient@example.com";
            string subject = "Hello from Papercut!";
            string body = $"This is a test email sent from {originalFileName}.";

            // Create the email message
            var mail = new MailMessage(fromAddress, toAddress, subject, body);

            // Create the SMTP client
            var smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.EnableSsl = false;

            try
            {
                // Send the email
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
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
