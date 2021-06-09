using Microsoft.Reporting.WinForms;
using System.Windows;

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


        //private static string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));

        public static string ContentStart ="Report.rdlc";

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            _reportviewer.RefreshReport();

        }
    }
}
