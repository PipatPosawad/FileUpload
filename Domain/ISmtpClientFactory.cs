using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ISmtpClientFactory
    {
        ISmtpClient Create(string smtpServer, int smtpPort);
    }
}
