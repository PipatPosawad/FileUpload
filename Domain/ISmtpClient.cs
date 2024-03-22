using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ISmtpClient : IDisposable
    {
        bool EnableSsl { get; set; }

        void Send(MailMessage mailMessage);
    }
}
