using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mailer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBClass db;

        public MainWindow()
        {
            InitializeComponent();
            //cbSenderSelect.ItemsSource = Model.VariablesClass.Senders;
            //cbSenderSelect.DisplayMemberPath = "Key";
            //cbSenderSelect.SelectedValuePath = "Value";

            cbSmtpSelect.ItemsSource = ServiceData.SmtpClients;
            cbSmtpSelect.DisplayMemberPath = "Host";
            cbSmtpSelect.Text = ServiceData.SmtpClients[0].Host;
            db = new DBClass();
            dgEmails.ItemsSource = db.Emails;
            tbcSender.ItemsSource = db.Senders;
        }


        //private void btnSend_Click(object sender, RoutedEventArgs e)
        //{
        //    EMailInfo info = new EMailInfo();
        //    info.Sender = cbFrom.Text;
        //    info.Body = tbBody.Text;
        //    info.Password = tbPassword.Password;
        //    info.Port = int.Parse(tbPort.Text);
        //    info.SmtpClient = tbServer.Text;
        //    info.Subject = tbSubject.Text;
        //    info.From = cbFrom.Text;
        //    info.To = cbTo.Text;
        //    EMailSendServiceClass eMailSendServiceClass = new EMailSendServiceClass();
        //    eMailSendServiceClass.Send(info);
        //    tbLog.Text += DateTime.Now + "\r\n";
        //    tbLog.Text += eMailSendServiceClass.Status + Environment.NewLine;
        //    tbLog.Text+=eMailSendServiceClass.ErrorInfo+ Environment.NewLine;

        //}

        private void MailPrepare(EMailInfo info,string To="")
        {
            //EMailInfo info = new EMailInfo();
            info.Sender = tbcSender.TextComboBox; //cbSenderSelect.Text;//cbFrom.Text;
            info.Body = rtbBody.Selection.Text;
            info.Password = tbPassword.Password;
            info.Port = int.Parse(tbPort.Text);
            info.SmtpClient = tbServer.Text;
            info.Subject = "None";//tbSubject.Text;
            info.From = tbcSender.TextComboBox;//cbSenderSelect.Text; //cbFrom.Text;
            info.To = To; //cbTo.Text;
            //EMailSendServiceClass eMailSendServiceClass = new EMailSendServiceClass();
            //eMailSendServiceClass.Send(info);
            //tbLog.Text += DateTime.Now + "\r\n";
            //tbLog.Text += eMailSendServiceClass.Status + Environment.NewLine;
            //tbLog.Text += eMailSendServiceClass.ErrorInfo + Environment.NewLine;
        }

        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlanner;
        }

        private void btnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            //string strLogin = cbSenderSelect.Text;
            //string strPassword = cbSenderSelect.SelectedValue.ToString();
            string strLogin = tbUserName.Text;
            string strPassword = tbPassword.Password;
            Console.WriteLine("Данные взяли из полей настроек");
            if (string.IsNullOrEmpty(strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }

            EMailInfo eMailInfo = new EMailInfo();
            MailPrepare(eMailInfo);//To должен заполняться в массовой рассылке из списка
            EMailSendServiceClass emailSender = new EMailSendServiceClass();
            emailSender.SendMailsOverThread((IQueryable<Email>)dgEmails.ItemsSource,eMailInfo);
        }

        private void btnSend2_Click(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);
            if (tsSendTime == new TimeSpan())
            {
                MessageBox.Show("Некорректный формат даты");
                return;
            }
            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if (dtSendDateTime < DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                return;
            }
            //EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());
            //sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void tscTabSwitcherControl_btnNextClick(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Next");
            tabControl.SelectedIndex = (tabControl.SelectedIndex + 1) % tabControl.Items.Count;
        }

        private void tscTabSwitcherControl_btnPreviousClick(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0) tabControl.SelectedIndex = tabControl.Items.Count - 1;
            else tabControl.SelectedIndex--;            
        }

        private void ToolBarChooser_btnAddClick(object sender, RoutedEventArgs e)
        {
            db.Senders.Add(tbcSender.TextComboBox);

        }

        private void ToolBarChooser_btnRemoveClick(object sender, RoutedEventArgs e)
        {
            db.Senders.Remove(tbcSender.TextComboBox);
            tbcSender.TextComboBox = "";
        }

        private void tbcSender_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
