using System;
using System.Globalization;
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
            while (true)
            {
                var text = Console.ReadLine();
                if (text == "quit") return;
                try
                {
                    Console.WriteLine(interpreter.Run(text));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
