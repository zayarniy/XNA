using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Model
{
    public class MyMailMessage: MailMessage
    {
        public MyMailMessage(string from,string to,string subject,string body):base(from,to,subject,body)
        {

        }
        public override string ToString()
        {
            return base.Subject;
        }
    }

    public class Item : INotifyPropertyChanged
    {
        private bool sent;
        private DateTime _dateTime;

        public DateTime DateTime { get => _dateTime;
            set
                {
                if (_dateTime != value)
                {
                    _dateTime = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTime"));
                }
            } }

        public MyMailMessage MailMessage { get; set; }

        public string Subject { get => MailMessage.Subject; }

        public bool Sent
        {
            get => sent; set
            {
                if (sent != value)
                {
                    sent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Sent"));
                }
            }
        }

        public Item(DateTime dateTime, MyMailMessage mailMessage)
        {
            this.DateTime = dateTime;
            this.MailMessage = mailMessage;
            this.Sent = true;//Отправлено
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            //return DateTime.ToLongDateString() + " " + DateTime.ToLongTimeString() + ":" + MailMessage.Body;
            return "Mail";
        }
    }


    public class Database
    {
        public ObservableCollection<Item> Items { get; set; }        

        public Database()
        {
            Items = new ObservableCollection<Item>();
            Items.Add(new Item(DateTime.Now, new MyMailMessage("from@mail.ru", "to@mail.ru", "subject", "Body1")));
            Items.Add(new Item(DateTime.Now, new MyMailMessage("from@mail.ru", "to@mail.ru", "subject", "Body2")));
            Items.Add(new Item(DateTime.Now, new MyMailMessage("from@mail.ru", "to@mail.ru", "subject", "Body3")));
        }



    }

}
