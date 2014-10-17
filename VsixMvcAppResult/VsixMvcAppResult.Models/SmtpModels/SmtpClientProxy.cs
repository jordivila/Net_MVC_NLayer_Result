using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using VsixMvcAppResult.Models.Configuration;

namespace VsixMvcAppResult.Models.SmtpModels
{
    public interface ISmtpClient : IDisposable
    {
        void Send(MailMessage mailMessage);
        void SendAsync(MailMessage mailMessage, Delegate callback);
    }

    public class SmtpClientProxy : ISmtpClient
    {
        //private SmtpClient _smtpClient;

        public SmtpClientProxy()
        {
            //_smtpClient = new SmtpClient();
        }

        public void Send(MailMessage mailMessage)
        {

            using (SmtpClient smtp = new SmtpClient()
            {
                Host = ApplicationConfiguration.MailingSettingsSection.SmtpHostName,
                Port = ApplicationConfiguration.MailingSettingsSection.SmtpHostPort,
                EnableSsl = ApplicationConfiguration.MailingSettingsSection.SmtpHostUseSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailMessage.From.Address, ApplicationConfiguration.MailingSettingsSection.SmtpHostPassword)
            })
            {
                smtp.Send(mailMessage);
            }
        }

        public void SendAsync(MailMessage mailMessage, Delegate callback)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //_smtpClient.Dispose();
        }
    }

    public class SmtpClientMock : ISmtpClient
    {
        public SmtpClientMock()
        {

        }

        public void Send(MailMessage mailMessage)
        {

        }

        public void Dispose()
        {

        }


        public void SendAsync(MailMessage mailMessage, Delegate callback)
        {
            throw new NotImplementedException();
        }
    }
}