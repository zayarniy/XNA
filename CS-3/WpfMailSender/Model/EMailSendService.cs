using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender.Model
{
    class EMailInfo
    {
        public string SmtpClient { get; set; }
        public int Port { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }

    class EMailSendService
    {
        public string Status { get; private set; } = "OK";
        public string ErrorInfo { get; private set; } = "";

        public bool Send(EMailInfo eMailInfo)
        {
            MailMessage mm = new MailMessage(eMailInfo.From, eMailInfo.To);
            mm.Subject = eMailInfo.Subject;
            mm.Body = eMailInfo.Body;
            mm.IsBodyHtml = false;

            // Авторизуемся на smtp-сервере и отправляем письмо
            SmtpClient sc = new SmtpClient(eMailInfo.SmtpClient, eMailInfo.Port);
            sc.EnableSsl = true;
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            sc.UseDefaultCredentials = false;
            sc.Credentials = new NetworkCredential(eMailInfo.Sender, eMailInfo.Password);
            try
            {
                sc.Send(mm);
            }
            catch (Exception exc)
            {
                Status = exc.Message;
                ErrorInfo = exc.StackTrace;
                return false;
            }
            Status = "OK";
            return true;
        }

    }
}
