namespace Shceme.Expression
{
    public abstract class ScmExpression
    {
        public EvalResult Eval(ScmEnvironment env)
        {
            var exp = this.EvalImpl(env).Value;
            var resultExp = exp;
            while (!(resultExp is SelfEvaluatingExpression) && !(resultExp is ProcedureExpression))
            {
                resultExp = resultExp.Eval(env).Value;
            }
            return resultExp.ToResult();
        }

        protected abstract EvalResult EvalImpl(ScmEnvironment env);
    }
}