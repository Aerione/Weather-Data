using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Väderdata___Inlämning
{
    internal class ReadWriteFile
    {

        public static string path = "../../../TextFile/";
        public static void ReadAll(string fileName)
        {
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                string line = reader.ReadLine();
                int rowCount = 0;
                while (line != null)
                {
                    Console.WriteLine(rowCount + " " + line);
                    rowCount++;
                    line = reader.ReadLine();
                }
            }
        }

        public static void WriteAll(string fileName, string text)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.WriteLine(text);
                for (int i  = 0; i < 10; i++)
                {
                    streamWriter.WriteLine(i);
                }
            }
        }


    }

}
