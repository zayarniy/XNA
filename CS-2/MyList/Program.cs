using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyList
{
    class OwnObject
    {
        public int Data { get; set; }
        public string Str { get; set; }
        public OwnObject Next { get; set; }


        public OwnObject(int data,string str)
        {
            this.Data = data;
            this.Str = str;
        }
        public override string ToString()
        {
            return Data + ":" + Str;
        }
    }

    class OwnList
    {
        OwnObject root;

        public OwnList()
        {
            root = null;
        }

        public void Add(OwnObject data)
        {
            data.Next = root;
            root = data;
        }

        public void Print()
        {
            OwnObject current = root;
            while(current!=null)
            {
                Console.WriteLine(current);
                current = current.Next;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OwnList ownList = new OwnList();
            ownList.Add(new OwnObject(1, "one"));
            ownList.Add(new OwnObject(2, "two"));
            ownList.Add(new OwnObject(3, "three"));
            ownList.Print();
            Console.ReadKey();
        }
    }
}
