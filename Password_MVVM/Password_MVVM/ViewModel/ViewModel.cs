using System;
using System.Collections.Generic;
using System.ComponentModel;//for INotifyPropertyChanged
using System.Linq;
using System.Runtime.CompilerServices;//for [CallerMemberName]
using System.Text;
using System.Threading.Tasks;

namespace Password_MVVM.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }

    class ViewModel: ViewModelBase
    {

        bool access;

        public Model.Account Account { get; set; } = new Model.Account("None", "None");

        Model.Accounts Accounts=new Model.Accounts();

        public void Check()
        {
            Access = Accounts.Checks(Account);
        }

        public bool Access
        {
            get
            {
                return Accounts.Checks(Account);
            }
            set
            {
                access = value;
                OnPropertyChanged("Access");
            }
        }
    }
}
