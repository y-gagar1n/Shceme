namespace Shceme.Expression
{
    public abstract class ScmExpression
    {
        public ScmExpression Eval(ScmEnvironment env)
        {
            var exp = this.EvalImpl(env);
            var resultExp = exp;
            while (!(resultExp is SelfEvaluatingExpression) && !(resultExp is ProcedureExpression))
            {
                resultExp = resultExp.Eval(env);
            }
            return resultExp;
        }

        protected abstract ScmExpression EvalImpl(ScmEnvironment env);
    }
}