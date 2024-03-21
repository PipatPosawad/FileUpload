using System;
using System.Threading.Tasks;
using Domain.BlobRepositories;
using Domain.Constants;
using Domain.Messages;
using Domain.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Service;

namespace FunctionApp;

/// <summary>
/// Function to send mail notification.
/// </summary>
public class MailNotificationFunction
{
    private readonly ILogger<MailNotificationFunction> _logger;
    private readonly IMailNotificationService _mailNotificationService;

    public MailNotificationFunction(ILogger<MailNotificationFunction> logger,
        IMailNotificationService mailNotificationService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _mailNotificationService = mailNotificationService ?? throw new ArgumentNullException(nameof(mailNotificationService));
    }

    [Function(nameof(MailNotificationFunction))]
    public async Task RunAsync([QueueTrigger(Queues.MailNotification, Connection = "AzureWebJobsStorage")] MailNotificationMessage message)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {message}");

        try
        {
            await _mailNotificationService.SendMailAsync(message.FileId);
        }
        catch (Exception exception)
        {
            _logger.LogError("{function}: Failed to send mail notification: {exception}",
                nameof(MailNotificationFunction), exception);
            throw;
        }
    }
}
