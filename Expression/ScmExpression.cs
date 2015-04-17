namespace Shceme.Expression
{
    public abstract class ScmExpression
    {
        public EvalResult Eval(ScmEnvironment env)
        {
            var result = this.EvalImpl(env);
            if (result.Success)
            {
                var exp = this.EvalImpl(env).Value;
                var resultExp = exp;
                while (!(resultExp is SelfEvaluatingExpression) && !(resultExp is ProcedureExpression))
                {
                    var innerResult = resultExp.Eval(env);
                    if (!innerResult.Success) return innerResult;
                    resultExp = resultExp.Eval(env).Value;
                }
                return resultExp.ToResult();
            }
            else
            {
                return result;
            }
        }

        protected abstract EvalResult EvalImpl(ScmEnvironment env);
    }
}