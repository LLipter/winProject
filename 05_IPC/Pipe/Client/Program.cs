using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            NamedPipeClientStream pipeClient = new NamedPipeClientStream("localhost", "testpipe", PipeDirection.Out);
            pipeClient.Connect();
            Console.WriteLine("Connection built");
            StreamWriter sw = new StreamWriter(pipeClient);
            sw.WriteLine("Hello, I'm LLipter!");
            sw.Flush();
            sw.Close();
            Console.WriteLine("Connection closed");
        }
    }
}
