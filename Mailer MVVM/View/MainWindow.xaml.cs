using Mailer.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
//Здесь для взаимодействия с ViewModel используется библиотека MS XAMLBehavior WPF
namespace Mailer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Из-за того, что Report не является элементов WPF приходится нарушать MVVM
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = MainVM;
            InitializeReport();

        }


        private void miReport_Click(object sender, RoutedEventArgs e)
        {
//            View.ReportWindow reportWindow = new View.ReportWindow();
//            reportWindow.ShowDialog();
        }



        private void InitializeReport()
        {
            _reportviewer.LocalReport.DataSources.Clear();
            var rpds_model = new ReportDataSource() { Name = "DataSet1", Value = MainVM.Database.Items };

            _reportviewer.LocalReport.DataSources.Add(rpds_model);
            _reportviewer.LocalReport.EnableExternalImages = true;
            _reportviewer.LocalReport.ReportPath = ContentStart;
            _reportviewer.SetDisplayMode(DisplayMode.PrintLayout);
//            _reportviewer.Refresh();
            _reportviewer.RefreshReport();
        }


        private static string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));

        public static string ContentStart = _path + @"\Mailer MVVM\Report.rdlc";

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            _reportviewer.RefreshReport();

        }
    }
}
