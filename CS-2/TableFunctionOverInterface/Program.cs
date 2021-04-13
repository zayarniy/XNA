using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFunctionOverInterface
{
    interface IFunc
    {
        double F(double x);
    }

    class TableFunc
    {

        public void Table(IFunc Func,double a, double b, double h)
        {
            double x = a;
            while (x <= b)
            {
                Console.WriteLine("{0,10}{1,10}", x, Func.F(x));
                x += h;
            }
        }
    }

    class F1 : IFunc
    {
        public double F(double X)
        {
            return X * X;
        }
    }

    class F2 : IFunc
    {
        public double F(double X)
        {
            return X * X* X;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
