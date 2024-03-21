using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public class MailNotificationSettings
    {
        public string SmtpServer { get; init; }

        public int SmtpPort { get; init; }

        public string FromAddress { get; init; }

        public string ToAddress { get; init; }

        public string Subject { get; init; }
    }
}
