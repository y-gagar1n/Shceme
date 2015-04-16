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
        public ScmExpression Operator { get; set; }
        public ScmExpression[] Arguments { get; set; }
        public ProcedureFactory _factory = new ProcedureFactory();

        public ApplicationExpression(ScmExpression @operator, ScmExpression[] arguments)
        {
            Operator = @operator;
            Arguments = arguments;
        }

        protected override ScmExpression EvalImpl(ScmEnvironment env)
        {
            var exp = @Operator.Eval(env);

            if (exp is ProcedureExpression)
            {
                var proc = exp as ProcedureExpression;
                object ve;

                object[] mappedOperands;
                if (proc.Proc is PrimitiveProcedure)
                {
                    mappedOperands =
                        Arguments.Select(x => x.Eval(env))
                            .OfType<SelfEvaluatingExpression>()
                            .Select(x => x.Value)
                            .ToArray();
                }
                else
                {
                    mappedOperands = MapValues(Arguments, env);
                    //var parameters = proc.Proc.Parameters.ToList();
                    //var newEnv = env.Extend();
                    //for (int i = 0; i < parameters.Count(); i++)
                    //{
                    //    newEnv.Dict[parameters[i]] = Arguments[i];
                    //}
                }

                ve = proc.Proc.Apply(mappedOperands);

                return new SelfEvaluatingExpression(ve);
            }
            else if (exp is ApplicationExpression)
            {
                var application = exp as ApplicationExpression;
                return application.Eval(env);
            }

            return exp;//new VoidExpression();
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
