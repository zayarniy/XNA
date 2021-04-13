using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MVVM_TestMvvmLightTools.Model;

namespace MVVM_TestMvvmLightTools.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            this.ClickAccess = new RelayCommand(Check, CanExecute);
            this.CloseWindowCommand = new RelayCommand<MainWindow>(this.CloseWindow);
        }

        public RelayCommand ClickAccess { get; private set; }
        public RelayCommand<MainWindow> CloseWindowCommand { get; private set; }

        private void CloseWindow(MainWindow window)
        {
            window?.Close();
        }

        public int AttemptCount { get; private set; } = 0;
        public Account Account { get; set; } = new Account("", "");

        void Check()
        {
            System.Console.WriteLine(AttemptCount);
            AttemptCount++;
            if (Model.Accounts.Checks(Account))
            {
                AttemptCount = -1;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Access allowed"));
            }
            RaisePropertyChanged("AttemptCount");
        }

        bool CanExecute()
        {
            return AttemptCount < 3 && AttemptCount != -1;
        }
    }
}