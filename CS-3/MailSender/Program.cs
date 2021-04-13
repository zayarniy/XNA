using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;


namespace MailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            MailMessage mm = new MailMessage("zaazaa@yandex.ru", "zaazaa@yandex.ru");
            mm.Subject = "Subject";
            mm.Body = "Body";
            mm.IsBodyHtml = false;
            // Авторизуемся на smtp-сервере и отправляем письмо
            SmtpClient sc = new SmtpClient("smtp.yandex.ru", 25);
            sc.EnableSsl = true;
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            sc.UseDefaultCredentials = false;
            sc.Credentials = new NetworkCredential("zaazaa@yandex.ru",                
                "12345687");
            try
            {
                sc.Send(mm);
                Console.WriteLine("Mail has sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }
    }
}
