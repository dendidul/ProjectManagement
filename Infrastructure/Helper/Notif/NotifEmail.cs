using Infrastructure.Helper.Config;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Helper.Notif
{
    public class NotifEmail : INotifEmail
    {
        private string _host;
        private string _username;
        private string _password;
        private int _port;
        private IConfigCreatorHelper _config;

        public NotifEmail(IConfigCreatorHelper config)
        {
            _host = config.Get("Notification:Email:Host.String");
            _username = config.Get("Notification:Email:SMTPUsername.String");
            _password = config.Get("Notification:Email:SMTPPassword.String");
            _port = config.GetInteger("Notification:Email:Port.Int");
        }

        public bool SendEmail(string mailFrom, string mailTo, string emailSubject, string emailMessage)
        {
            var result = false;

            try
            {
                //using (var message = new MailMessage())
                //{
                //    if (mailTo.Contains(","))
                //    {
                //        var mailToSplit = mailTo.Split(",");

                //        foreach (var to in mailToSplit)
                //        {
                //            message.To.Add(new MailAddress(to));
                //        }
                //    }
                //    else if (mailTo.Contains(";"))
                //    {
                //        var mailToSplit = mailTo.Split(";");

                //        foreach (var to in mailToSplit)
                //        {
                //            message.To.Add(new MailAddress(to));
                //        }
                //    }
                //    else
                //    {
                //        message.To.Add(new MailAddress(mailTo));
                //    }

                //    message.From = new MailAddress(mailFrom);
                //    message.Subject = emailSubject;
                //    message.Body = emailMessage;
                //    message.IsBodyHtml = true;

                //    using (var client = new SmtpClient(_host))
                //    {
                //        client.Port = _port;
                //        client.Credentials = new NetworkCredential(_username, _password);

                //        client.Send(message);
                //    }
                //}

                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(mailFrom));
                email.To.Add(MailboxAddress.Parse(mailTo));
                email.Subject = emailSubject;
                email.Body = new TextPart(TextFormat.Html) { Text = emailMessage };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect(_host, _port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_username, _password);
                smtp.Send(email);
                smtp.Disconnect(true);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
