using System;
using System.IO;

namespace Tracer.tracer.output
{
    public class FileOutPut  :OutPutTracerResult
    {
        private static  string path =System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private const string directory = @"data\";
        public void output(string result)
        {
            string name =@"result"+( new Random(1000).Next()).ToString()+".txt";
            Console.WriteLine(path.Substring(0, path.LastIndexOf(@"\Tracer")+8)+directory+name);
            using (FileStream fstream = new FileStream(path.Substring(0, path.LastIndexOf(@"\Tracer")+8)+directory+name, FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(result);
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
            }
        }
    }
}