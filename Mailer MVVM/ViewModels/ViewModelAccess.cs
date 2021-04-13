using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mailer.Commands;

namespace Mailer.ViewModels
{
    public class ViewModelAccess : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelAccess()
        {

        }

        #region Публичное свойство для привязки Account
        public Model.Account Account { get; set; } = new Model.Account("None", "None");

        public int AttemptCount
        {
            get
            {
                return Model.AccessToApp.Attempt;
            }
        }
        #endregion


        private void Execute(object obj)
        {
            //Проверяем есть ли такой аккаунт в базе данных
            bool flag=Model.AccessToApp.Checks(Account);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AttemptCount"));
            if (flag)
            {
                MainWindow wnd = new MainWindow();

                wnd.Show();
            }

        }

        private bool CanExecute(object obj)
        {
            return Model.AccessToApp.Attempt < 3;
        }

        public ICommand ClickAccess
        {
            get
            {
                return new DelegateCommand(Execute, CanExecute);
            }
        }
    }
}
