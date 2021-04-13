using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading;

namespace Mailer.Model
{

    //Альтернативный класс для отправки
    static class ServiceData
    {
        //static public List<string> SmptClients = new List<string>()
        //{ "smtp.yandex.ru", "smtp.gmail.com","smtp.mail.ru" };
        //    static public List<int> Ports = new List<int>() { 25, 58, 25 };
        static public List<SmtpClient> SmtpClients { get; } = new List<SmtpClient>() { new SmtpClient("smtp.yandex.ru", 25), new SmtpClient("smtp.gmail.com", 58), new SmtpClient("smtp.mail.ru", 25) };

    }

    class Schedule
    {
        DispatcherTimer dispatcherTimer;

        public void Start()
        {
            dispatcherTimer.Start();
        }

        public void Stop()
        {
            dispatcherTimer.Stop();
        }

        public Schedule(EventHandler dispTimer,int seconds)
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, seconds);
            dispatcherTimer.Tick += new EventHandler( dispTimer);
            //dispatcherTimer.Tick += DispatcherTimer_Tick;
            //dispatcherTimer.Start();
        }

    }

    class EMailSendServiceClass 
    {


        static public event Action<string> Action;//Любое событие может вызываться только внутри класса в котором оно определено

        private static string log;//Журнал

        public static string GetLog()
        {
            return log;
        }

        public static void SetLog(string value)
        {
            log = value;
            try
            {
                System.IO.File.WriteAllText("log.txt", log);
                //Action?.Invoke("Log has been written");
            }
            catch (Exception exc)
            {
                //Action?.Invoke(exc.Message);
                Console.WriteLine(DateTime.Now + ":" + exc + "\r\n");
            }
        }

        public EMailSendServiceClass()
        {
            Action += EMailSendServiceClass_Action;
            if (System.IO.File.Exists("log.txt"))
            {
                log = System.IO.File.ReadAllText("log.txt");
            }
        }

        private void EMailSendServiceClass_Action(string message)
        {
            SetLog(log+DateTime.Now + ":" + message + "\r\n");
            Console.WriteLine(DateTime.Now + ":" + message + "\r\n");
        }

        public bool Send(MailMessage message)//, string password, int port)
        {
            try
            {
                string subject = message.Subject;// tbSubject.Text;
                string password = System.IO.File.ReadAllText("C:\\temp\\1.txt");
                int port=587;
                string body = message.Body;
                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("geekbrains2021@gmail.com", password)
                };
                Action?.Invoke("Message is sending");
                smtp.Send(message);
                //MessageBox.Show("Message has sent");
                //Debug.WriteLine("Message has sent");
                Action?.Invoke("Message has sent");
                Console.WriteLine("From:"+message.From);
                Console.WriteLine("To:"+message.To);
                Console.WriteLine("Body:"+message.Body);
                return true;
            }
            catch (Exception ex)
            {
                //using System.Diagnostic
                Debug.WriteLine(ex);
                //Console.WriteLine(ex);
                Action?.Invoke(ex.Message);
                return false;
            }

        }

        public bool Check(string body)
        {
            return !(string.IsNullOrEmpty(body));
        }


    }
}
