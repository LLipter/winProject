using System;
using CSharpLibrary;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Invoke method in CSMath library!");
            int[] array = { 1, 2, 3, 4, 5 };
            int sum = CSMath.sum(array);
            Console.WriteLine("Sum of array = {0}", sum);
        }
    }
}
