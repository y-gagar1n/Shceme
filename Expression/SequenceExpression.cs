using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    public class SequenceExpression : ScmExpression
    {
        private ScmExpression[] _exprs;

        public SequenceExpression(ScmExpression[] exprs)
        {
            _exprs = exprs;
        }


        protected override ScmExpression EvalImpl(ScmEnvironment env)
        {
            ScmExpression result = null;
            foreach (var scmExpression in _exprs)
            {
                result = scmExpression.Eval(env);
            }
            return result;
        }
    }
}
