using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shceme.Procedure;

namespace Shceme.Expression
{
    public class ApplicationExpression : ScmExpression
    {
        public VariableExpression Operator { get; set; }
        public ScmExpression[] Arguments { get; set; }
        public ProcedureFactory _factory = new ProcedureFactory();

        public ApplicationExpression(VariableExpression @operator, ScmExpression[] arguments)
        {
            Operator = @operator;
            Arguments = arguments;
        }

        public override ScmExpression Eval(ScmEnvironment env)
        {
            var proc = @Operator.Eval(env) as ProcedureExpression;
            object ve;

            object[] mappedOperands;
            if (proc.Proc is PrimitiveProcedure)
            {
                mappedOperands = Arguments.Select(x => x.Eval(env)).OfType<SelfEvaluatingExpression>().Select(x => x.Value).ToArray();
            }
            else
            {
                var parameters = proc.Proc.Parameters.ToList();
                var newEnv = env.Extend();
                for (int i = 0; i < parameters.Count(); i++)
                {
                    newEnv.Dict[parameters[i]] = Arguments[i];
                }
                mappedOperands = MapValues(Arguments, newEnv);
            }

            ve = proc.Proc.Apply(mappedOperands); 

            return new SelfEvaluatingExpression(ve);
        }

        private object[] MapValues(ScmExpression[] exps, ScmEnvironment env)
        {
            return exps.Select(x => x.Eval(env)).Select(x =>
            {
                if (x is SelfEvaluatingExpression)
                {
                    return (x as SelfEvaluatingExpression).Value;
                }
                else
                {
                    return x;
                }
            }).ToArray();
        }
    }
}
