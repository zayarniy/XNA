using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mailer
{
    /// <summary>
    /// Interaction logic for ToolBarChooser.xaml
    /// </summary>
    [ContentProperty("Text")]//Показать заменой Content 
    public partial class ToolBarChooser : UserControl
    {





        public ImageSource Add
        {
            get
            {
                return iAdd.Source;
            }
            set
            {
                iAdd.Source = value;
            }
        }

        public ImageSource Remove
        {
            get
            {
                return iRemove.Source;
            }
            set
            {
                iRemove.Source = value;
            }
        }

        public ImageSource Edit
        {
            get
            {
                return iEdit.Source;
            }
            set
            {
                iEdit.Source = value;
            }
        }



        public string Text
        {
            get
            {
                return tbSender.Text;
            }
            set
            {
                tbSender.Text = value;
            }
        }

        public string TextComboBox
        {
            get
            {
                return cbSenderSelect.Text;
            }
            set
            {
                cbSenderSelect.Text = value; 
            }
        }

        public IEnumerable ItemsSource
        {
            get
            {
                return cbSenderSelect.ItemsSource;
            }
            set
            {
                cbSenderSelect.ItemsSource = value;
            }
        }



        public event RoutedEventHandler btnAddClick;
        public event RoutedEventHandler btnRemoveClick;
        public event RoutedEventHandler btnEditClick;

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            btnAddClick?.Invoke(sender, e);
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            btnRemoveClick?.Invoke(sender, e);
        }

        public ToolBarChooser()
        {
            InitializeComponent();
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnEditClick?.Invoke(sender, e);
        }
    }
}
