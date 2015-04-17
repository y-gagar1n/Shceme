using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shceme.Expression;

namespace Shceme
{
    public class EvalResult
    {
        public ScmExpression Value { get; set; }

        public EvalResult(ScmExpression value)
        {
            Value = value;
        }

        public static EvalResult From(ScmExpression exp)
        {
            return new EvalResult(exp);
        }
    }

    static class EvalResultHelpers
    {
        public static EvalResult ToResult(this ScmExpression exp)
        {
            return EvalResult.From(exp);
        }
    }
}
