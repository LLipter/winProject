using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COM;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass myClass = new MyClass();
            int a = myClass.Multiply(5, 3);
            Console.WriteLine(a);
        }
    }
}

