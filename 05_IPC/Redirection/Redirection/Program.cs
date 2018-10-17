using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirection
{
    class Program
    {

        static void ExecuteCmd(string cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            // 是否使用外壳程序   
            process.StartInfo.UseShellExecute = false;
            // 重定向输入流  
            process.StartInfo.RedirectStandardInput = true;
            // 重定向输出流
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            // 获取输出信息   
            string result = process.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
            process.WaitForExit();
            process.Close();
        }

        static void Main(string[] args)
        {
            ExecuteCmd("getmac");
            ExecuteCmd("shutdown /s /t 3600"); // shuwdown computer in 3600 seconds
            ExecuteCmd("shutdown /a");  // cancel shutdown plan



        }
    }
}
