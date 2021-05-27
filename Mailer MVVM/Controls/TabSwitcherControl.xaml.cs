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

namespace Mailer
{
    /// <summary>
    /// Interaction logic for TabSwitchControl.xaml
    /// </summary>
    public partial class TabSwitcherControl : UserControl
    {
        public TabSwitcherControl()
        {
            InitializeComponent();
            btnPrevious.Click += BtnPrevious_Click;
            btnNext.Click += BtnNext_Click;

        }



        #region properties
        private bool bHidebtnPrevious = false; // поле, соответствующее тому, будет ли скрыта кнопка «Предыдущий»
        private bool bHideBtnNext = false; // поле, соответствующее тому, будет ли скрыта кнопка «Следующий»
        /// <summary>
        /// Свойство, соответствующее тому, будет ли скрыта кнопка «Предыдущий». 
        /// Чтобы Свойство отразилось на PropertiesGrid у нашего контрола, его нужно представить именно свойством, а не полем
        /// </summary>
        public bool IsHidebtnPrevious
        {
            get { return bHidebtnPrevious; }
            set
            {
                bHidebtnPrevious = value;
                SetButtons(); // метод, который отвечает на отрисовку кнопок
            }
        }// IsHidebtnPrevious
        public bool IsHideBtnNext
        {
            get { return bHideBtnNext; }
            set
            {
                bHideBtnNext = value;
                SetButtons(); // метод, который отвечает за отрисовку кнопок
            }
        }// IsHidebtnPrevious
        private void BtnNextTruebtnPreviousFalse()
        {
            btnNext.Visibility = Visibility.Hidden;
            btnPrevious.Visibility = Visibility.Visible;
            btnPrevious.Width = 229;
            btnNext.Width = 0;
            btnPrevious.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void btnPreviousTrueBtnNextFalse()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Visible;
            btnNext.Width = 229;
            btnPrevious.Width = 0;
            btnNext.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void btnPreviousFalseBtnNextFalse()
        {
            btnNext.Visibility = Visibility.Visible;
            btnPrevious.Visibility = Visibility.Visible;
            btnNext.Width = 115;
            btnPrevious.Width = 114;
        }
        private void btnPreviousTrueBtnNextTrue()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Метод, который отвечает за отрисовку кнопок.
        /// Есть три варианта: когда обе кнопки доступны; доступна одна и недоступна вторая; обе кнопки недоступны
        /// </summary>
        private void SetButtons()
        {
            if (bHidebtnPrevious && bHideBtnNext) btnPreviousTrueBtnNextTrue();
            else if (!bHideBtnNext && !bHidebtnPrevious) btnPreviousFalseBtnNextFalse();
            else if (bHideBtnNext && !bHidebtnPrevious) BtnNextTruebtnPreviousFalse();
            else if (!bHideBtnNext && bHidebtnPrevious) btnPreviousTrueBtnNextFalse();
        }
        #endregion




        #region DependencyProperty
        public static readonly DependencyProperty PrevTextProperty = DependencyProperty.Register("PrevText", typeof(string), typeof(TabSwitcherControl), new PropertyMetadata("Prev"));

        public static readonly DependencyProperty NextTextProperty = DependencyProperty.Register("NextText", typeof(string), typeof(TabSwitcherControl), new PropertyMetadata("Next"));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(TabSwitcherControl));

        public object CommandParameter
        {
            get
            {
                return (object)GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public string PrevText
        {
            get
            {
                return (string)GetValue(PrevTextProperty);
            }
            set
            {
                SetValue(PrevTextProperty, value);
            }
        }

        public string NextText
        {
            get
            {
                return (string)GetValue(PrevTextProperty);
            }
            set
            {
                SetValue(PrevTextProperty, value);
            }
        }

        #endregion



        #region RoutedEvents
        public static readonly RoutedEvent btnPrevClickEvent = EventManager.RegisterRoutedEvent("btnPreviousClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabSwitcherControl));

        public static readonly RoutedEvent btnNextClickEvent = EventManager.RegisterRoutedEvent("btnNextClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabSwitcherControl));



        #endregion

        public event RoutedEventHandler btnPreviousClick
        {
            add { AddHandler(btnPrevClickEvent, value); }
            remove { RemoveHandler(btnPrevClickEvent, value); }
        }


        public event RoutedEventHandler btnNextClick
        {
            add { AddHandler(btnPrevClickEvent, value); }
            remove { RemoveHandler(btnPrevClickEvent, value); }
        }




        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            CommandPrev.Execute(this);
            e.Handled = true;
            RoutedEventArgs args = new RoutedEventArgs(btnPrevClickEvent);
            RaiseEvent(args);
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            CommandNext.Execute(this);
            e.Handled = true;
            RoutedEventArgs args = new RoutedEventArgs(btnNextClickEvent);
            RaiseEvent(args);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (oldContent != null)
                throw new InvalidOperationException("You can't change Content!");
        }


        //Для того, чтобы можно было передовать команды
        public ICommand CommandNext
        {
            get { return (ICommand)GetValue(CommandNextProperty); }
            set { SetValue(CommandNextProperty, value); }
        }

        public static readonly DependencyProperty CommandNextProperty =
            DependencyProperty.Register("CommandNext", typeof(ICommand), typeof(TabSwitcherControl), new UIPropertyMetadata(null));

        public ICommand CommandPrev
        {
            get { return (ICommand)GetValue(CommandPrevProperty); }
            set { SetValue(CommandPrevProperty, value); }
        }

        public static readonly DependencyProperty CommandPrevProperty =
            DependencyProperty.Register("CommandPrev", typeof(ICommand), typeof(TabSwitcherControl), new UIPropertyMetadata(null));
    }
}
