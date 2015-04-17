using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    public class VariableExpression : ScmExpression
    {
        private string _variableName;

        public string VariableName
        {
            get { return _variableName; }
        }

        public VariableExpression(string variableName)
        {
            _variableName = variableName;
        }

        protected override EvalResult EvalImpl(ScmEnvironment env)
        {
            var result = env.Lookup(_variableName);
            return (result is ScmExpression ? result as ScmExpression : new SelfEvaluatingExpression(result)).ToResult();
        }
    }
}
