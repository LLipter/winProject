using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Word
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Environment.CurrentDirectory + "\\test.docx";
            if (File.Exists(filePath))
                File.Delete(filePath);

            Application app = new Application();
            Document doc = app.Documents.Add();









            doc.SaveAs2(filePath);
            app.Quit();

        }
    }
}
