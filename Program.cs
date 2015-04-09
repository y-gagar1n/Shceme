using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Shceme.Expression;
using Shceme.Procedure;

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

    public class ScmInterpreter
    {
        private ExpressionFactory _factory;
        private ScmEnvironment _env;

        public ScmInterpreter()
        {
            _factory = new ExpressionFactory();
            _env = new ScmEnvironment();
            
            _env.Add("+", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc + x)));
            _env.Add("*", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc * x, 1.0)));
            _env.Add("-", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc - x).TransformFirst((x, n) => n > 1 ? -x : x)));
            _env.Add(">", new ProcedureExpression(new BooleanProcedure<double>((x1, x2) => x1 > x2)));
            _env.Add("<", new ProcedureExpression(new BooleanProcedure<double>((x1, x2) => x1 < x2)));
            _env.Add("=", new ProcedureExpression(new BooleanProcedure<double>((x1, x2) => x1 == x2)));
        }
        public string Run(string text)
        {
            var exp = _factory.Create(text);

            var resultExp = exp.Eval(_env) as SelfEvaluatingExpression;

            return resultExp.ToString();
        }
    }

    public abstract class ScmExpression
    {
        //public ScmProcedure Operator { get; set; }
        //public object[] arguments { get; set; }

        public abstract ScmExpression Eval(ScmEnvironment env);
    }

    public class ScmEnvironment
    {
        private Dictionary<string, object> _dict = new Dictionary<string, object>();

        public Dictionary<string, object> Dict
        {
            get { return _dict; }
        }

        public string[] Names
        {
            get { return _dict.Keys.ToArray(); }
        }

        public object[] Values
        {
            get { return _dict.Values.ToArray(); }
        }

        public void Add(string key, object value)
        {
            _dict.Add(key, value);
        }

        public ScmEnvironment Parent { get; private set; }

        public ScmEnvironment Extend()
        {
            return new ScmEnvironment {Parent = this};
        }

        public object Lookup(string variableName)
        {
            if (_dict.ContainsKey(variableName))
            {
                return _dict[variableName];
            }
            else if (Parent != null)
            {
                return Parent.Lookup(variableName);
            }
            else return null;
        }
    }
}
