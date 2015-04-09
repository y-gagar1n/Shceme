using Shceme.Expression;
using Shceme.Procedure;

namespace Shceme
{
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
}