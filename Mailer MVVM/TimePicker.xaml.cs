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

namespace Mailer
{
    /// <summary>
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl,INotifyPropertyChanged
    {
        
        //Убрать hour,minute и second связав с полями date?
        DateTime date=DateTime.Now;

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
                if (date!=value)
                {
                    date = new DateTime(value.Year, value.Month, value.Day, Hour, Minute, Second);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));

                }
            }
        }
        public int Hour
        {
            get
            {
                return date.Hour;
            }
            set
            {
                if (hour != value) 
                {
                    hour = value;
                    Date = new DateTime(Date.Year, Date.Month, Date.Day, Hour, Minute, Second);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hour"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
                }
            }
        }

        public int Minute
        {
            get
            {
                return date.Minute;
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
                return date.Second;
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

        public int[] Numbers60 { get; } = new int[60];
        public int[] Numbers24 { get; } = new int[24];
        public TimePicker()
        {
            InitializeComponent();
            for (int i = 0; i < 60; i++) Numbers60[i] = i;
            for (int i = 0; i < 24; i++) Numbers24[i] = i;
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return new DateTime(Date.Year,Date.Month,Date.Day, Hour, Minute, Second).ToString();
        }
    }
}
