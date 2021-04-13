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
using System.Diagnostics;

namespace Password_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ViewModel.ViewModel ViewModel { get; set; } = new ViewModel.ViewModel();
        //ViewModel.ViewModel viewModel = new ViewModel.ViewModel();

        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = this.viewModel;
        }

        private void btnAccess_Click(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine(viewModel.Account.Login);
            //Debug.WriteLine(viewModel.Account.Password);
            //BindingExpression expression = tbAccess.GetBindingExpression(TextBlock.TextProperty);
            //expression.UpdateTarget();
            //viewModel.Check();
            //ViewModel.Check();
        }
    }
}
