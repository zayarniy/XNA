using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class MyClass
    {
        int a;
    }

    class Database<T>
    {
        T[] data;
    }

    class Program
    {



        //static void Swap(ref int A,ref int B)
        //{
        //    int T = A;
        //    A = B;
        //    B = T;
        //}

        //static void Swap(ref double A,ref double B)
        //{
        //    double T = A;
        //    A = B;
        //    B = T;
        //}

        static void Swap<T>(ref T A,ref T B)
        {
            T t = A;
            A = B;
            B = t;
        }

        static void Main(string[] args)
        {
            int x = 3, y = 4;
            double e = 4, t = 5;
            Swap<int>(ref x,ref y);
            Swap<double>(ref e, ref t);
            MyClass a = new MyClass(), b = new MyClass();
            Swap<MyClass>(ref a, ref b);
            List<int> list = new List<int>();
            List<MyClass> list2 = new List<MyClass>();
        }
    }
}
