using System.IO;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateAssembly.Run(
                Path.Combine(@"C:\BackUps\", Path.GetFileNameWithoutExtension("myAssembly.dll") + ".dll"),
                "myAssembly.dll");
        }
    }
}
