using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CsClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Guid guid = new Guid("EAFD6DD6-E49A-480D-A57D-43A38132A252");
            Type type = Type.GetTypeFromCLSID(guid);
            object obj = Activator.CreateInstance(type);
            object[] para = new object[] { 1, 2 };
            int result = (int)type.InvokeMember("Add",BindingFlags.InvokeMethod, null, obj, para);
            Console.WriteLine(result);
        }
    }
}
