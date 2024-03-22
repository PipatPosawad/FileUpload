using Domain;
using Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SmtpClientFactory : ISmtpClientFactory
    {
        public ISmtpClient Create(string smtpServer, int smtpPort)
        {
            return new SmtpClientWrapper(smtpServer, smtpPort);
        }
    }
}
