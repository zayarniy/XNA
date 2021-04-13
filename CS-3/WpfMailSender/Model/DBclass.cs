using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender.Model
{
    class DBclass
    {
        private RecipientsDataContext recipients = new RecipientsDataContext();

        public IQueryable<Table> Recipients
        {
            get
            {
                return from c in recipients.Tables select c;
            }
        }

    }
}
