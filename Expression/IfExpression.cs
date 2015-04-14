using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    public class IfExpression : ScmExpression
    {
        private ScmExpression _predicate;
        private ScmExpression _then;
        private ScmExpression _else;

        public IfExpression(ScmExpression predicate, ScmExpression then, ScmExpression @else)
        {
            _else = @else;
            _then = then;
            _predicate = predicate;
        }

        protected override ScmExpression EvalImpl(ScmEnvironment env)
        {
            if (True(_predicate.Eval(env)))
            {
                return _then.Eval(env);
            }
            else
            {
                return _else.Eval(env);
            }
        }

        private bool True(ScmExpression exp)
        {
            var selfExp = exp as SelfEvaluatingExpression;
            return selfExp != null && selfExp.Value is bool && (bool) selfExp.Value;
        }
    }
}
