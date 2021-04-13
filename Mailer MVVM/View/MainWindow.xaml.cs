using Mailer.Model;
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

        //ViewModels.ViewModelMain ViewModel { get; set; } = new ViewModels.ViewModelMain();
        public MainWindow()
        {
            InitializeComponent();
          //  tbTime.ItemsSource = ViewModel.Database.Items;
//            this.DataContext = ViewModel;

        }

        private void tscTabSwitcherControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
