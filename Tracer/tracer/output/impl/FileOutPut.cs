using System;
using System.IO;

namespace Tracer.tracer.output
{
    public class FileOutPut : IOutPutTracerResult
    {
        private static  string _path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private const string Directory = @"data\";
        public void output(string result)
        {
            string name =@"result"+( new Random(1000).Next()).ToString()+".txt";
            using (FileStream fstream = new FileStream(_path.Substring(0, _path.LastIndexOf(@"\Tracer")+8)+Directory+name, FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(result);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}