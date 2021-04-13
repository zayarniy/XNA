using System;
using System.Collections.Generic;
using System.IO;
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

namespace TextTokenizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        byte[] _buffer;

        public MainWindow()
        {
            InitializeComponent();
            TokenCounter tokenCounter = new TokenCounter(tbFilename.Text);
            //dgTable.DataContext = tokenCounter.WordCounts;

        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            UpdateProgress("Reading file");
            FileStream inputStream = new FileStream(tbFilename.Text, FileMode.Open);
            _buffer = new byte[inputStream.Length];
            StreamWriter streamWriter = new StreamWriter("data.txt");            
            //pass in the inputStream as the argument to the "Done" method
            IAsyncResult result = inputStream.BeginRead(_buffer, 0, _buffer.Length, FileReadDone, inputStream);
            //the IAsyncResult object can be used to track the progress of the method

            //while the file reading is going on, we can do other work, like click buttons or exit the program

        }

        private void FileReadDone(IAsyncResult result)
        {
            UpdateProgress("File read done");
            FileStream inputStream = result.AsyncState as FileStream;
            inputStream.Close();

            //start async tokenizing
            TokenCounter counter = new TokenCounter(Encoding.ASCII.GetString(_buffer));
            IAsyncResult counterResult = counter.BeginCount(CountDone, counter);
            UpdateProgress("Counting tokens");
            //if we want to wait for it to finish, call:
            //counter.EndCount(counterResult);
        }

        private void CountDone(IAsyncResult result)
        {
            UpdateProgress("Count done");
            TokenCounter counter = result.AsyncState as TokenCounter;
            this.Dispatcher.Invoke(()=> dgTable.ItemsSource = counter.WordCounts);
        }

        private void UpdateProgress(string message)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                this.tbStatus.Text = message;
            }));
        }
    }
}
