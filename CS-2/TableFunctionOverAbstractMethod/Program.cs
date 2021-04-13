using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFunctionOverAbstractMethod
{

    abstract class TableFunc
    {

        protected abstract double F(double x);

        public void Print(double a, double b, double h)
        {
            double x = a;
            while (x <= b)
            {
                Console.WriteLine("{0,10}{1,10}", x, F(x));
                x += h;
            }
        }

    }

    class F1: TableFunc
    {
        protected override double F(double x)
        {
            return x * x * x;
        }
    }

    class F2 : TableFunc
    {
        protected override double F(double x)
        {
            return x * x * x*x;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TableFunc table = new F2();
            table.Print(0, 10, 1);

            Console.ReadKey();
        }
    }
}
