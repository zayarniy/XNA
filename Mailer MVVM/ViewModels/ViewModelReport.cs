using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mailer.ViewModels
{
    class ViewModelReport
    {
        //private View.ReportWindow _window;
        //private LocalReport _Report;
        private ReportViewer _reportviewer;
        

        public ViewModelReport(View.ReportWindow window)
        {
          //  _window = window;
          //Задача - перебросить ссылку из UI в класс в котором происходит инициализация Report
            this._reportviewer = window._reportviewer;
            Items = (window.DataContext as ViewModels.ViewModelMain).Database.Items;
            Initialize();

        }

        public IEnumerable<Mailer.Model.Item> Items { get; set; }
         /*=new List<Mailer.Model.Item>()
        {
            new Model.Item(DateTime.Now,new Model.MyMailMessage("mail@mail.ru","mail@mail.ru","Subject","Body"))
        };
         */

        private void Initialize()
        {
            _reportviewer.LocalReport.DataSources.Clear();
            var rpds_model = new ReportDataSource() { Name = "DataSet1", Value = Items };

            _reportviewer.LocalReport.DataSources.Add(rpds_model);
            _reportviewer.LocalReport.EnableExternalImages = true;
            _reportviewer.LocalReport.ReportPath = ContentStart;
            _reportviewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportviewer.Refresh();
            _reportviewer.RefreshReport();
        }


        private static string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));

        public static string ContentStart = _path + @"\Mailer MVVM\Report.rdlc";

    }
}
