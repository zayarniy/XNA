using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        static UserControl1()
        {
            ButtonNextActionEvent = EventManager.RegisterRoutedEvent("ButtonNextAction", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserControl1));
            ButtonPrevActionEvent = EventManager.RegisterRoutedEvent("ButtonPrevAction", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserControl1));
        }

        //public event EventHandler<EventArgs> ButtonPrevAction;
        //public event EventHandler<EventArgs> ButtonNextAction;

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    ButtonNextAction.Invoke(sender, e);
        //}

        //private void btnPrev_Click(object sender, EventArgs e)
        //{
        //    ButtonPrevAction.Invoke(sender, e);
        //}



        public event RoutedEventHandler ButtonNextAction
        {
            add { this.AddHandler(ButtonNextActionEvent, value); }
            remove { this.RemoveHandler(ButtonNextActionEvent,value); }
        }

        public event RoutedEventHandler ButtonPrevAction
        {
            add { this.AddHandler(ButtonPrevActionEvent, value); }
            remove { this.RemoveHandler(ButtonPrevActionEvent, value); }
        }

        static public readonly RoutedEvent ButtonNextActionEvent;
        static public readonly RoutedEvent ButtonPrevActionEvent;


        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(ButtonPrevActionEvent);
            RaiseEvent(args);

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(ButtonNextActionEvent);
            RaiseEvent(args);
        }


    }
}
