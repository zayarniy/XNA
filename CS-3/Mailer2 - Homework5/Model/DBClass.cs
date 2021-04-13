using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
    public class DBClass
    {
        private EmailsDataContext emails = new EmailsDataContext();

        //private List<string> senderList = new List<string>() { "none@gmail.com" };
        private ObservableCollection<string> senderList = new ObservableCollection<string>() { "none@none.no" };

        public ObservableCollection<string> Senders
        {
            get { return senderList; }
        }    

        public void AddSender(string sender)
        {
            senderList.Add(sender);
        }

        public IQueryable<Email> Emails
        {
            get
            {
                return from c in emails.Emails select c;
            }
        }
    }
}
