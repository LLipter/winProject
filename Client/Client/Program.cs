using System;
using System.Reflection;
using System.Runtime.InteropServices;
using CSharpLibrary;

namespace Client
{
    class Program
    {
        [DllImport("../../CppLibrary/x64/Release/CppLibrary.dll", EntryPoint = "getMax")]
        public static extern int getMax(int a, int b);

        static void Main(string[] args)
        {
            Console.WriteLine("Invoke method in C# library by adding reference!");
            int[] array = { 1, 2, 3, 4, 5 };
            int sum = CSMath.sum(array);
            Console.WriteLine("Sum of array = {0}", sum);

            Console.WriteLine("Invoke method in C# library by loading dynamically!");
            Assembly a = Assembly.LoadFrom("../../CSharpLibrary/CSharpLibrary/bin/debug/netstandard2.0/CSharpLibrary.dll");
            Type lib = a.GetType("CSharpLibrary.CSMath");
            MethodInfo mi = lib.GetMethod("sum");
            object o = Activator.CreateInstance(lib);
            Object[] parameters = new Object[1];
            parameters[0] = array;
            sum = (int) mi.Invoke(o, parameters);
            Console.WriteLine("Sum of array = {0}", sum);



            Console.WriteLine("Invoke method in C++ library!");
            Console.WriteLine(getMax(1, 2));



        }
    }
}
