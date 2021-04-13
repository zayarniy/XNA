using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;//for INotifyPropertyChanged
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Specialized;

namespace MVVM_Test.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {

        DelegateCommand ClickCommand;
        DelegateCommand TextCommand;

        public ViewModel()
        {
            ClickCommand=new DelegateCommand(Execute, CanExecute);
            TextCommand = new DelegateCommand(Execute2, CanExecute2);
        }
        public event PropertyChangedEventHandler PropertyChanged;
       
        bool access;
        int attemptCount = 0;

        #region Публичное свойство для привязки "кол-во попыток"
        public int AttemptCount
        {
            get
            { return attemptCount; }

            private set
            {
                if (attemptCount != value)
                {

                    attemptCount = value;
                    //Уведомление о том, что свойство изменилось(для обновления пользовательского интерфейса)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AttemptCount"));
                }
            }
        }
        #endregion

        public Model.Account Account { get; set; } = new Model.Account("", "");

        Model.Accounts Accounts = new Model.Accounts();

        public ICommand ClickAccess
        {
            get
            {
                return ClickCommand;
            }
        }
        public ICommand TextCmd
        {
            get
            {
                return TextCommand;
            }
        }

        private void Execute(object obj)
        {
            Access = Accounts.Checks(Account);            
            AttemptCount++;
        }

        private void Execute2(object obj)
        {
            //Access = Accounts.Checks(Account);
            //AttemptCount++;
            Console.WriteLine("1");
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
        private bool CanExecute(object obj)
        {
            return AttemptCount < 3;
        }

        private bool CanExecute2(object obj)
        {
            return Account.Password.Length < 20;
        }

    }
}
