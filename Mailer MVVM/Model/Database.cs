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
        public MyMailMessage(string str1,string str2,string str3,string str4):base(str1,str2,str3,str4)
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

        //public override string ToString()
        //{
        //    return DateTime.ToLongDateString() + " " + DateTime.ToLongTimeString() + ":" + MailMessage.Body;
        //}
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
