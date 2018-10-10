using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace COM
{


    [ComVisible(true)]
    [Guid("7B152689-B156-4DCC-8B04-B5E78589E29B")]
    public interface IMyClass
    {
        void Initialize();
        void Dispose();
        int Multiply(int x, int y);
    }

    [ComVisible(true)]
    [Guid("63310A03-04C0-43A9-AAA1-6A24727F0B38")]
    [ProgId("COM.MyClass")]
    public class MyClass : IMyClass
    {
        public void Initialize()
        {
            // nothing to do  
        }

        public void Dispose()
        {
            // nothing to do  
        }

        public int Multiply(int x, int y)
        {
            return x * y;
        }
    }


}
