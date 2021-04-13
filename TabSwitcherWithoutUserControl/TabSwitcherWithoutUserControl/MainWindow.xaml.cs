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

namespace TabSwitcherWithoutUserControl
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


        private void tscUserControl_ButtonNextAction(object sender, RoutedEventArgs e)
        {
            tcTabControl.SelectedIndex = (tcTabControl.SelectedIndex + 1) % tcTabControl.Items.Count;

        }

        private void tscUserControl_ButtonPrevAction(object sender, RoutedEventArgs e)
        {
            if (tcTabControl.SelectedIndex == 0) tcTabControl.SelectedIndex = tcTabControl.Items.Count - 1;
            else tcTabControl.SelectedIndex--;

        }
    }
}
