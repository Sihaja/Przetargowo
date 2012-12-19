using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.ExternalAccess.Message;
using System.Net.Mail;

namespace Przetargi.ExternalAccess.Email
{
    public interface IMailer
    {
        MessageBase SendMail(IEnumerable<string> to, string subject, string body);
        MessageBase SendMail(MailMessage message);
    }
}
