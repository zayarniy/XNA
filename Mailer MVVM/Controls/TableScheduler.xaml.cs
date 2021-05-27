using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Mailer.Model;

namespace Mailer
{
    /// <summary>
    /// Interaction logic for TableScheduler.xaml
    /// </summary>
    public partial class TableScheduler : UserControl, INotifyPropertyChanged
    {

        //Убрать hour,minute и second связав с полями date?
        DateTime date = DateTime.Now;

        public int[] Numbers60 { get; } = new int[60];
        public int[] Numbers24 { get; } = new int[24];

        public TableScheduler()
        {
            for (int i = 0; i < 60; i++) Numbers60[i] = i;
            for (int i = 0; i < 24; i++) Numbers24[i] = i;
            InitializeComponent();
            this.DataContext = this;
            Console.WriteLine("TableScheduler was crearted");
            //InitializeComponent();            
        }



        int hour;
        int minute;
        int second;
        
        
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                if (date != value)
                {
                    date = new DateTime(value.Year, value.Month, value.Day, Hour, Minute, Second);
                    this.Hour = value.Hour;
                    this.Minute = value.Minute;
                    this.Second = value.Second;
                    //cldSchedulDateTimes.SelectedDate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hour"));
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Second"));

                }
            }
        }
        public int Hour
        {
            get
            {
                return hour;
            }
            set
            {
                //if (date.Hour != value)
                {
                    hour = value;
                    Date = new DateTime(Date.Year, Date.Month, Date.Day, Hour, Minute, Second);
                    //cldSchedulDateTimes.SelectedDate = date;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hour"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
                }
            }
        }

        public int Minute
        {
            get
            {
                return minute;
            }
            set
            {
                if (minute != value)
                {
                    minute = value;
                    Date = new DateTime(Date.Year, Date.Month, Date.Day, Hour, Minute, Second);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Minute"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
                }
            }
        }
        public int Second
        {
            get
            {
                return second;
            }
            set
            {
                if (second != value)
                {
                    second = value;
                    Date = new DateTime(Date.Year, Date.Month, Date.Day, Hour, Minute, Second);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Second"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(TableScheduler));//,new FrameworkPropertyMetadata(null,new PropertyChangedCallback(OnItemsSourceChanged)));


        private static void OnItemsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
           // Console.WriteLine("Items source was changed");
            //o.SetValue(ItemsSourceProperty, e.NewValue);
        }

        public IEnumerable<object> ItemsSource
        {
            get
            {
                return (IEnumerable<object>)GetValue(ItemsSourceProperty);
            }
            set
            {
                //Console.WriteLine("Items source changed");
                SetValue(ItemsSourceProperty, value);
            }
        }

        public override string ToString()
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, Hour, Minute, Second).ToString();
        }

        private void DgGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DateTime date=((sender as DataGrid).SelectedItem as Mailer.Model.Item).DateTime;
            Console.WriteLine(date);
            this.Date = date;
        }

        //Именно из-за того, что было не понятно, как пробросить с помощью MVVM доступ к отдельной ячейке dataGrid, было принято 
        //решение создать отдельный контрол
        private void CldSchedulDateTimes_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
           // if (dgGrid == null) return;
           
            var Item = (dgGrid.SelectedItem as Item);
            if (Item == null && cldSchedulDateTimes.SelectedDate!=null) return;
            Item.DateTime = cldSchedulDateTimes.SelectedDate.Value;
            Console.WriteLine("SelectedDatesChanged");
            
        }
    }
}
