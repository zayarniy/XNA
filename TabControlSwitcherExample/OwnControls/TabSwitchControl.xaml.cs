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

namespace OwnControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TabSwitchControl : UserControl
    {
        public TabSwitchControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler ButtonPrevAction;
        public event RoutedEventHandler ButtonNextAction;

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            ButtonPrevAction?.Invoke(sender, e);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ButtonNextAction?.Invoke(sender, e);
        }
    }
}
