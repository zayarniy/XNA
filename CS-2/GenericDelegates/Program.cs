using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegates
{

    //delegate bool IS<T>(T c);//сигнатура bool(char)
    //delegate bool Predicate<T>(T c);//сигнатура bool(char)
    class Program
    {



        static int CountChar(string s, Predicate<char> isChar)
        {
            int count = 0;
            foreach (char c in s)
                if (isChar(c)) count++;
            return count;
        }


        //static int CountCharLat(string s)
        //{
        //    int count = 0;
        //    foreach (char c in s)
        //        if (c>='A' && c<='Z' || c >= 'a' && c <= 'z') count++;
        //    return count;
        //}

        //static int CountCharRus(string s)
        //{
        //    int count = 0;
        //    foreach (char c in s)
        //        if (c >= 'А' && c <= 'Я' || c >= 'а' && c <= 'я') count++;
        //    return count;
        //}


        static bool IsRus(char c)
        {
            return c >= 'А' && c <= 'Я' || c >= 'а' && c <= 'я' || c == 'ё' || c == 'Ё';
        }

        static bool IsLat(char c)
        {
            return c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z';
        }



        static void Main(string[] args)
        {
            string str = "на ловца и зверь бежит. избавились отприведения типов"+
                "on the catcher and the beast runs. got rid of typecasting";
            Console.WriteLine(CountChar(str,new Predicate<char>(IsRus)));
            Console.WriteLine(CountChar(str, IsRus));            
            Console.WriteLine(CountChar(str, IsLat));
            Console.WriteLine(CountChar(str,char.IsDigit));
        }
    }
}
