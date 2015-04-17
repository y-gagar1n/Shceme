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

        protected override EvalResult EvalImpl(ScmEnvironment env)
        {
            var exp = @Operator.Eval(env).Value;

            if (exp is ProcedureExpression)
            {
                var proc = exp as ProcedureExpression;
                object ve;

                object[] mappedOperands;
                if (proc.Proc is PrimitiveProcedure)
                {
                    mappedOperands =
                        Arguments.Select(x => x.Eval(env).Value)
                            .OfType<SelfEvaluatingExpression>()
                            .Select(x => x.Value)
                            .ToArray();
                }
                else
                {
                    mappedOperands = MapValues(Arguments, env);
                }

                var applyResult = proc.Proc.Apply(mappedOperands);
                if (applyResult.Success)
                {
                    return new SelfEvaluatingExpression(applyResult.Value).ToResult();
                }
                else
                {
                    string msg = "";
                    if (@Operator is VariableExpression)
                    {
                        string procedureName = (@Operator as VariableExpression).VariableName;
                        msg = String.Format("Error while applying procedure {0}:{1}", procedureName,
                            Environment.NewLine);
                    }
                    
                    return EvalResult.Error(msg + applyResult.ErrorMessage);
                }
            }
            else if (exp is ApplicationExpression)
            {
                var application = exp as ApplicationExpression;
                return application.Eval(env);
            }

            return exp.ToResult();
        }

        private object[] MapValues(ScmExpression[] exps, ScmEnvironment env)
        {
            return exps.Select(x => x.Eval(env).Value).Select(x =>
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
