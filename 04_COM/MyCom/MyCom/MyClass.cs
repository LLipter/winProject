using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCom
{

    [ComVisible(true)]
    [Guid("48CF4385-5FC5-4DA2-9D1F-43EECBE754D5")]
    public interface IMyClass
    {
        int Add(int x, int y);
    }


    [ComVisible(true)]
    [Guid("EAFD6DD6-E49A-480D-A57D-43A38132A252")]
    [ProgId("MyCom.MyClass")]
    public class MyClass : IMyClass
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }  

}
