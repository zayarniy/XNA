using System;
using System.Collections.Generic;
using System.IO;
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

namespace DataGeneratorForHW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model.DataGenerator dataGenerator = new Model.DataGenerator();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = dataGenerator;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    Parallel.For(0, dataGenerator.Count, (i) =>
                      {
                          //Console.WriteLine(i);
                          try
                          {
                              string filename = System.IO.Path.GetDirectoryName(dataGenerator.TemplateFilename) + System.IO.Path.GetFileNameWithoutExtension(dataGenerator.TemplateFilename) + i + System.IO.Path.GetExtension(dataGenerator.TemplateFilename);
                              StreamWriter sw = new StreamWriter(filename);
                              sw.WriteLine("{0} {1} {2}", dataGenerator.Random.Next(1, 3), dataGenerator.Random.NextDouble(), dataGenerator.Random.NextDouble());
                              //Thread.Sleep(100);//Чтобы увидеть изменения текста
                              sw.Close();
                              this.Dispatcher.Invoke(() => tbStatus.Text = filename);
                          }
                          catch (Exception exc)
                          {
                              Console.WriteLine(exc.Message);
                          };
                      });
                    tbStatus.Text = "Done";
                });
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}

