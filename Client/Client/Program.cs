using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;
using CSharpLibrary;
using Microsoft.Win32;


namespace Client
{
    class Program
    {
        [DllImport("../../../../CppLibrary/Release/CppLibrary.dll", EntryPoint = "getMax")]
        public static extern int getMax(int a, int b);

        static void Main(string[] args)
        {
            Console.WriteLine("Invoke method in C# library by adding reference!");
            int[] array = { 1, 2, 3, 4, 5 };
            int sum = CSMath.sum(array);
            Console.WriteLine("Sum of array = {0}", sum);

            Console.WriteLine("Invoke method in C# library by loading dynamically!");
            Assembly a = Assembly.LoadFrom("../../../../CSharpLibrary/CSharpLibrary/bin/debug/netstandard2.0/CSharpLibrary.dll");
            Type lib = a.GetType("CSharpLibrary.CSMath");
            MethodInfo mi = lib.GetMethod("sum");
            object o = Activator.CreateInstance(lib);
            Object[] parameters = new Object[1];
            parameters[0] = array;
            sum = (int)mi.Invoke(o, parameters);
            Console.WriteLine("Sum of array = {0}", sum);
            Console.WriteLine();


            Console.WriteLine("Invoke method in C++ library!");
            Console.WriteLine(getMax(1, 2));
            Console.WriteLine();


            // Referece
            // https://www.cnblogs.com/boby-/p/4724255.html

            Console.WriteLine("Registry Manipulation!");
            RegistryKey hklm = Registry.LocalMachine;
            Console.WriteLine("Create Key");
            Console.WriteLine(hklm);
            RegistryKey hkSoftWare = hklm.CreateSubKey(@"SOFTWARE\test");
            hklm.Close();
            hkSoftWare.Close();

            Console.WriteLine("Open Key");
            hklm = Registry.LocalMachine;
            hkSoftWare = hklm.OpenSubKey(@"SOFTWARE\test", true);
            hklm.Close();
            hkSoftWare.Close();

            Console.WriteLine("Delete Key");
            hklm = Registry.LocalMachine;
            hklm.DeleteSubKey(@"SOFTWARE\test", true);
            hklm.Close();


        }
    }
}
