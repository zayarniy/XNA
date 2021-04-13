using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Mailer
{
    class SchedulerClass
    {
        DispatcherTimer timer = new DispatcherTimer(); // таймер 
        EMailSendServiceClass emailSender;         // экземпляр класса, отвечающего за отправку писем
        DateTime dtSend;                           // дата и время отправки
        IQueryable<Email> emails;                  // коллекция email-ов адресатов

        //*******
        public static string Messages { get; private set; }

        /// <summary>
        /// Метод, который превращает строку из текстбокса tbTimePicker в TimeSpan
        /// </summary>
        /// <param name="strSendTime"></param>
        /// <returns></returns>
        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }
            return tsSendTime;
        }
        /// <summary>
        //// Непосредственно отправка писем
        /// </summary>
        /// <param name="dtSend"></param>
        /// <param name="emailSender"></param>
        /// <param name="emails"></param>
        public void SendEmails(DateTime dtSend, EMailSendServiceClass emailSender, IQueryable<Email> emails)
        {
            this.emailSender = emailSender; // Экземпляр класса, отвечающего за отправку писем, присваиваем 
            this.dtSend = dtSend;
            this.emails = emails;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dtSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                //emailSender.SendMails(emails);
                timer.Stop();
                Messages+=DateTime.Now+":Письма отправлены.\r\n";
            }
        }

    }
}
