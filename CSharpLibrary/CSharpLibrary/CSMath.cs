using System;

namespace CSharpLibrary
{
    public class CSMath
    {
        public static int max(int a,int b)
        {
            return a > b ? a : b;
        }

        public static int min(int a, int b)
        {
            return a < b ? a : b;
        }

        public static int sum(int[] array)
        {
            int result = 0;
            foreach(int i in array)
            {
                result += array[i];
            }
            return result;
        }
    }
}
