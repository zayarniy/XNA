using System;
using System.Collections.Generic;
using System.ComponentModel;//for INotifyPropertyChanged
using System.Linq;
using System.Runtime.CompilerServices;//for [CallerMemberName]
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Password_MVVM.ViewModel
{


    public class ViewModel: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        bool access;
        int attemptCount = 0;
        public int AttemptCount
        {
            get
            { return attemptCount; }

            private set
            {
                if (attemptCount != value)
                {
                    attemptCount = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AttemptCount"));
                }
            }
        }

        public Model.Account Account { get; set; } = new Model.Account("None", "None");

        Model.Accounts Accounts=new Model.Accounts();

        public void Check()
        {
            Access = Accounts.Checks(Account);
        }

        public ICommand ClickAccess
        {
            get
            {
                return new DelegateCommand((obj) =>
                    {
                        Check();
                        AttemptCount++;
                    }, (obj) => AttemptCount < 3
                ); ;
            }
        }

        public bool Access
        {
            get
            {
                return access;
            }
            set
            {
                if (access != value)
                {
                    access = value;                    
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Access"));
                }
            }
        }

        
    }
}
