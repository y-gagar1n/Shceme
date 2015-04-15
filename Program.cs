using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shceme
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new ScmInterpreter();

            if (args.Length == 2 && args[0] == "-f")
            {
                var filePath = Path.GetFullPath(args[1]);
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        interpreter.Run(line);
                    }
                    Console.WriteLine("{0} loaded", filePath);
                }
                else
                {
                    Console.WriteLine("{0} not found", filePath);
                }
            }

            while (true)
            {
                var text = Console.ReadLine();
                if (text == "quit") return;
                try
                {
                    string result = interpreter.Run(text);
                    if(result != null) Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
