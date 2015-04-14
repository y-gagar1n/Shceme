using System;
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
            _env.Add("*", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc * x, 1.0)));
            _env.Add("-", new ProcedureExpression(AggregateProcedure<double>.Create((acc, x) => acc - x).TransformFirst((x, n) => n > 1 ? -x : x)));
            _env.Add(">", new ProcedureExpression(new BinaryProcedure<double,bool>((x1, x2) => x1 > x2)));
            _env.Add("<", new ProcedureExpression(new BinaryProcedure<double,bool>((x1, x2) => x1 < x2)));
            _env.Add("=", new ProcedureExpression(new BinaryProcedure<double, bool>((x1, x2) => x1 == x2)));
            _env.Add("and", new ProcedureExpression(new AggregateProcedure<bool>((acc, x) => acc && x, true)));
            _env.Add("or", new ProcedureExpression(new AggregateProcedure<bool>((acc, x) => acc || x, false)));
            _env.Add("not", new ProcedureExpression(new UnaryProcedure<bool, bool>(x => !x)));
        }
        public string Run(string text)
        {
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