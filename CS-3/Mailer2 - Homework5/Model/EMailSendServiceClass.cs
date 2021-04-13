using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Threading;

namespace Mailer
{


//Из методички
    class emailSendServiceClass
    {
        #region vars
        private string strLogin;         // email, c которого будет рассылаться почта
        private string strPassword;  // пароль к email, с которого будет рассылаться почта
        private string strSmtp = "smtp.yandex.ru"; // smtp-server
        private int iSmtpPort = 25;                // порт для smtp-server
        private string strBody;                    // текст письма для отправки
        private string strSubject;                 // тема письма для отправки
        #endregion
        public emailSendServiceClass(string sLogin, string sPassword)
        {
            strLogin = sLogin;
            strPassword = sPassword;
        }

        private void SendMail(string mail, string name) // Отправка email конкретному адресату
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = "Hello world!";
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Невозможно отправить письмо " + ex.ToString());
                }
            }
        }//private void SendMail(string mail, string name)
        public void SendMails(IQueryable<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendMail(email.Value, email.Name);
            }
        }
    }  //private void SendMail(string mail, string name)


    //Альтернативный класс для отправки
    static class ServiceData
    {
        //static public List<string> SmptClients = new List<string>()
        //{ "smtp.yandex.ru", "smtp.gmail.com","smtp.mail.ru" };
        //    static public List<int> Ports = new List<int>() { 25, 58, 25 };
        static public List<SmtpClient> SmtpClients { get; } = new List<SmtpClient>() { new SmtpClient("smtp.yandex.ru", 25), new SmtpClient("smtp.gmail.com", 58), new SmtpClient("smtp.mail.ru", 25) };

    }


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

    class EMailSendServiceClass
    {

        public string Status { get; private set; } = "OK";
        public string ErrorInfo { get; private set; } = "";

        public class InfoForThread
        {
            public IQueryable<Email> emails { get; set; }
            public EMailInfo info { get; set; }
        }



        public void SendMails(IQueryable<Email> emails,EMailInfo info)
        {
            foreach (Email email in emails)
            {
                info.To = email.Value;//Кому
                SendMail(info);
            }
        }

        //Передача через Thread
        public void SendMailsOverThread(IQueryable<Email> emails, EMailInfo info)
        {
            Thread thread=new Thread(() =>{                
                SendMails(emails, info);
            });
            try
            {
                thread.Start();
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc);                
            }
        }


        public bool SendMail(EMailInfo eMailInfo)
        {
            MailMessage mm = new MailMessage(eMailInfo.From,eMailInfo.To);
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
            catch(Exception exc)
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
