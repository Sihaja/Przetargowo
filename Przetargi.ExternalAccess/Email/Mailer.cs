using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.ExternalAccess.Message;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace Przetargi.ExternalAccess.Email
{
    public class Mailer : IMailer
    {
        private static readonly string MailingHost = "smtp.gmail.com";
        private static readonly int MailingPort = 587;
        private static readonly string MailingUser = "przetargowo.pl@gmail.com";
        private static readonly string MailingDisplayUser = "Przetargowo.pl";
        private static readonly string MailingPassword = "przetargi88";

        public MessageBase SendMail(IEnumerable<string> to, string subject, string body)
        {
            return SendMail(new MailMessage(MailingUser, String.Join(";", to), subject, body));
        }

        public MessageBase SendMail(MailMessage message)
        {
            MessageBase response = new MessageBase { Result = ResultType.Success };

            try
            {
                message.From = new MailAddress(MailingUser, MailingDisplayUser);

                SmtpClient client = new SmtpClient
                {
                    Host = MailingHost,
                    Port = MailingPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(MailingUser, MailingPassword)
                };

                client.Send(message);
            }
            catch (Exception exc)
            {
                response.Result = ResultType.Failure;
                response.Message = exc.ToString();
            }

            return response;
        }
    }
}
