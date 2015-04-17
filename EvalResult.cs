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

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public EvalResult(ScmExpression value)
        {
            Success = true;
            Value = value;
        }

        public EvalResult(string message)
        {
            Success = false;
            ErrorMessage = message;
        }

        public static EvalResult From(ScmExpression exp)
        {
            return new EvalResult(exp);
        }

        public static EvalResult Error(string message)
        {
            return new EvalResult(message);
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
