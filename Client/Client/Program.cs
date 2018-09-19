using System;
using System.Reflection;
using CSharpLibrary;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Invoke method in CSMath library by adding reference!");
            int[] array = { 1, 2, 3, 4, 5 };
            int sum = CSMath.sum(array);
            Console.WriteLine("Sum of array = {0}", sum);

            Console.WriteLine("Invoke method in CSMath library by loading dynamically!");
            Assembly a = Assembly.LoadFrom("../../CSharpLibrary/CSharpLibrary/bin/debug/netstandard2.0/CSharpLibrary.dll");




            Type lib = a.GetType("CSharpLibrary.CSMath");
        
            MethodInfo mi = lib.GetMethod("sum");
            object o = Activator.CreateInstance(lib);
            Object[] parameters = new Object[1];
            parameters[0] = array;
            sum = (int) mi.Invoke(o, parameters);
            Console.WriteLine("Sum of array = {0}", sum);


            
        }
    }
}
