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
using System.Windows.Shapes;

namespace Mailer.View
{
    /// <summary>
    /// Interaction logic for ViewLogin.xaml
    /// </summary>
    public partial class ViewLogin : Window
    {
        ViewModels.ViewModelAccess ViewModelAccess { get; set; } = new ViewModels.ViewModelAccess();
        public ViewLogin()
        {
            InitializeComponent();
            this.DataContext = ViewModelAccess;
        }
    }
}
