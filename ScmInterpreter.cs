using System;
using System.IO;
using System.Runtime.ExceptionServices;
using Shceme.Expression;
using Shceme.Procedure;

namespace Shceme
{
    public class ScmInterpreter
    {
        private ExpressionFactory _factory;
        private ScmEnvironment _env;
        private string command = "";

        public ScmInterpreter()
        {
            _factory = new ExpressionFactory();
            _env = new ScmEnvironment();
            
            _env.Add("+", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc + x)));
            _env.Add("*", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc * x)));
            _env.Add("-", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc - x, (first, n) => n > 1 ? first : -first)));
            _env.Add("/", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc / x)));
            _env.Add(">", new ProcedureExpression(new BinaryProcedure<double,bool>((x1, x2) => x1 > x2)));
            _env.Add("<", new ProcedureExpression(new BinaryProcedure<double,bool>((x1, x2) => x1 < x2)));
            _env.Add("=", new ProcedureExpression(new BinaryProcedure<double, bool>((x1, x2) => x1 == x2)));
            _env.Add("and", new ProcedureExpression(new AggregateProcedure<bool>((acc, x) => acc && x)));
            _env.Add("or", new ProcedureExpression(new AggregateProcedure<bool>((acc, x) => acc || x)));
            _env.Add("not", new ProcedureExpression(new UnaryProcedure<bool, bool>(x => !x)));

            LoadModule("main");
        }

        private void LoadModule(string name)
        {
            var modulesFolder = Path.Combine("Modules", name + ".scm");
            var filePath = Path.GetFullPath(modulesFolder);
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    Run(line);
                }
                Console.WriteLine("{0} module loaded", name);
            }
        }

        public string Run(string text)
        {
            if (String.IsNullOrEmpty(text)) return null;

            command += text;

            switch(CheckBrackets(command))
            {
                case BracketsCheckResult.OK:
                    var exp = _factory.Create(command);
                    command = "";
                    ScmExpression resultExp = exp.Eval(_env);
                    return (resultExp as SelfEvaluatingExpression).ToString();
                case BracketsCheckResult.TooMuch:
                    command = "";
                    return "Too many closing brackets";
                default:
                    command += " ";
                    return null;
            }
        }

        private BracketsCheckResult CheckBrackets(string text)
        {
            int level = 0;
            foreach (var c in text)
            {
                switch (c)
                {
                    case '(':
                        level++;
                        break;
                    case ')':
                        level--;
                        if (level < 0) return BracketsCheckResult.TooMuch;
                        break;
                }
            }

            return level == 0 ? BracketsCheckResult.OK : BracketsCheckResult.NotYet;
        }
    }
}