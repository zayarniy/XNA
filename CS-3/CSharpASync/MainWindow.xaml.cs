using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CSharpASync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string DoWork()
        {
            Thread.Sleep(2000);
            return "Done with work!";
        }

        private Task<string> DoWorkAsync()
        {
            Task<string> task = new Task<string>(DoWork);
            task.Start();
            return  task;
        }

        private async void btnButton_Click(object sender, RoutedEventArgs e)
        {
            //Task.Factory.StartNew(()=>  this.Dispatcher.Invoke(delegate() { tbText.Text = DoWork(); }));
            tbText.Text = await DoWorkAsync();
        }
    }
}
