using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Windows.Input;
using Mailer.Model;
using Mailer.Commands;
using System.Windows.Controls;

namespace Mailer.ViewModels
{
    public class ViewModelMain : INotifyPropertyChanged
    {
        private string login = "zaazaa@yandex.ru";
        private string subject = "Hello";
        private string to = "none@none.ru";
        private string from = "none@none.ru";
        private string body = "";
        private int tickCount = 0;

        DelegateCommand NextTab;
        DelegateCommand PrevTab;

        //реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelMain()
        {
            NextTab = new DelegateCommand((obj) =>
            {
                Console.WriteLine("Next");
                TabSwitcherControl tsc = (obj as TabSwitcherControl);
                TabControl tc = (tsc.CommandParameter as TabControl);//using System.Windows.Controls;
                tc.SelectedIndex = (tc.SelectedIndex + 1) % tc.Items.Count;
            });
            PrevTab = new DelegateCommand((obj) =>
            {
                Console.WriteLine("Prev");
                TabSwitcherControl tsc = (obj as TabSwitcherControl);
                TabControl tc = (tsc.CommandParameter as TabControl);
                if (tc.SelectedIndex == 0) tc.SelectedIndex = tc.Items.Count - 1;
                else tc.SelectedIndex--;
            });
            Model.Schedule schedule = new Model.Schedule(Tick, 5);
            schedule.Start();
        }

        void Tick(object send, EventArgs args)
        {
            SendAll();
            TickCount++;
            //Console.WriteLine(TickCount);
        }

        public int TickCount
        {
            get => tickCount; set
            {
                if (tickCount != value)
                {
                    tickCount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TickCount"));
                }
            }
        }
        Model.EMailSendServiceClass EmailSendServiceClass { get; set; } = new Model.EMailSendServiceClass();

        public Model.Database Database { get => database; set => database = value; }

        public List<Item> Databas { get; set; } =new List<Item>();
        //public List<string> Databas { get; set; } = new List<string>() { "1", "2", "3" };

        public int Port { get; set; } = 25;//Пока используется только для демонстрации валидации

        public string Log//Журнал работы программы
        {
            get
            {
                return Model.EMailSendServiceClass.GetLog();
            }
            set
            {
                Model.EMailSendServiceClass.SetLog(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Log"));
            }
        }

        public List<string> FromList { get; set; } = new List<string>() { "zaazaa@yandex.ru", "putin@kremlin.ru" };
        public string Body
        {
            get => body;
            set
            {
                body = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Body"));
            }
        }
        public string From
        {
            get => from;
            set
            {
                from = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("From"));
            }
        }
        public string To
        {
            get => to;
            set
            {
                to = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("To"));
            }
        }
        public List<string> ToList { get; set; } = new List<string>() { "zaazaa@yandex.ru", "putin@kremlin.ru" };
        public string Subject
        {
            get => subject;
            set
            {
                subject = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Subject"));
            }
        }
        //public string Adress { get; set; }

        public string Login
        {
            get => login;
            set
            {
                login = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Login"));
            }
        }

        #region Создание команды - Версия 1
        private void Execute(object obj)
        {
            //Проверяем есть ли такой аккаунт в базе данных
            //Access = EmailSendServiceClass.Check(Body);

            //if (Access)
            {
                //  GetAccess = true;
                MailMessage mm = new MailMessage(From, To, Subject, Body);
                EmailSendServiceClass.Send(mm);
                //Mailer.MainWindow mailerWindow = new Mailer.MainWindow();
            }

        }

        private bool CanExecute(object obj)
        {
            return EmailSendServiceClass.Check(Body);
        }


        //Need using System.Windows.Input
        public ICommand Send
        {
            get
            {
                return new DelegateCommand(Execute, CanExecute);
            }
        }
        #endregion

        #region Создание команды - Версия 2 (без использования дополнительных методов)

        //public ICommand ClickAccess
        //{
        //    get
        //    {
        //        return new DelegateCommand((obj) =>
        //            {
        //                Check();
        //                AttemptCount++;
        //            }, (obj) => AttemptCount < 3
        //        ); ;
        //    }
        //}
        #endregion

        public ICommand ClickSchedule
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Console.WriteLine("Schedule click");
                }, (obj) => true);
            }
        }

        public ICommand ToDoTask
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Console.WriteLine("Pressed");
                    DateTime dt = ((DateTime)obj);
                    Console.WriteLine("Запланировано at " + dt.ToLongDateString() + " " + dt.ToLongTimeString());
                    Database.Items.Add(new Model.Item(dt, new MyMailMessage(from, to, subject, body)));
                },
                (obj) => ((obj != null) && ((DateTime)obj) > DateTime.Now) && Check());
            }

        }

        public ICommand SendAtOnce
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    EmailSendServiceClass.Send(new MailMessage(From, To, Subject, Body));
                    Console.WriteLine("Message has sent");
                },
                (obj) => Check());
            }
        }

        public ICommand AddNewMail
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Database.Items.Add(new Model.Item(DateTime.Now, new MyMailMessage(From, To, Subject, Body)));
                    Console.WriteLine("Message has sent");
                },
                (obj) => Check());
            }
        }


        void SendAll()
        {
            DateTime now = DateTime.Now;
            foreach (var mail in Database.Items)
                if (!mail.Sent && mail.DateTime <= now)
                {
                    
                    mail.Sent = EmailSendServiceClass.Send(mail.MailMessage);
                    Console.WriteLine("Message has "+((mail.Sent)?"":"not")+" sent");
                }
        }

        public ICommand SendAllAtOnce
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    SendAll();

                },
                (obj) =>

                {
                    //Проверяем есть ли не отправленные сообщения
                    foreach (var el in Database.Items)
                    {
                        if (el.Sent == false)
                        {
                            return true;
                        }
                    }
                    return false;
                });
            }

        }

        bool Check()
        {
            return !(String.IsNullOrEmpty(From) || String.IsNullOrEmpty(To) || String.IsNullOrEmpty(Login) /*|| String.IsNullOrEmpty(Adress)*/);
        }


        private int tabControlIndex = 0;
        private Database database = new Model.Database();

        public int TabControlIndex
        {
            get
            {
                return tabControlIndex;
            }
            set
            {
                if (tabControlIndex != value)
                {
                    tabControlIndex = value;
                    Console.WriteLine("Invoke");
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TabControlIndex"));
                }
            }
        }

        public ICommand TabSwitcherPrev
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                    TabControlIndex = TabControlIndex == 0 ? 5 : --TabControlIndex;
                    Console.WriteLine("Prev " + tabControlIndex);

                },
                (obj) => true);
            }
        }

        public ICommand ClickNext => NextTab;

        public ICommand ClickPrev => PrevTab;

    }
}
