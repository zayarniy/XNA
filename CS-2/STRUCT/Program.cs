using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STRUCT
{
    class Time
    {
        //fieds - поля
        private int hour,minute,second;

        public string Luck { get; set; }

        public Time(int hour,int minute,int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            SetHour(hour);
        }

        //метод
        public void SetHour(int value)
        {
            hour = value;
        }

        public int GetHour()
        {
            return hour;
        }

        //свойства
        public int Hour
        {
            //акцесор доступа
            get
            {
                return hour;
            }
            set
            {
                if (hour < 0 || hour > 23)
                    throw new ArgumentOutOfRangeException("Время задано не верно");
                this.hour = value;

            }

        }

        public  string ToString()
        {
            return String.Format($"{hour}:{minute}:{second}");
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            Time A, B;
            A = new Time(23, 31, 30);
            A.Hour = 20;
            //B = A;//данные переместятся из A в B (struct)
            B = A;//в B поместится ссылка на A(class)
            A.Hour = 21;
            //A.SetHour(20)
            //Console.WriteLine(A.GetHour());
            Console.WriteLine(B.Hour);
            Console.WriteLine(A);
            Console.ReadKey();
            //A.hour = 25;
            //A.minute = 500;
            //A.second = 100;

        }
    }
}
