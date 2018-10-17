using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

         
            while (true)
            {
                NamedPipeServerStream pipServer = new NamedPipeServerStream("testpipe", PipeDirection.In);
                Console.WriteLine("Wait for connection");
                pipServer.WaitForConnection();
                Console.WriteLine("Connection built");
                StreamReader sr = new StreamReader(pipServer);
                string msg = sr.ReadToEnd();
                Console.WriteLine(msg);
                pipServer.Close();
            }
        }
    }
}
