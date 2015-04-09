namespace Shceme.Expression
{
    public abstract class ScmExpression
    {
        public abstract ScmExpression Eval(ScmEnvironment env);
    }
}