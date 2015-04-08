using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shceme.Expression;

namespace Shceme.Procedure
{
    public class ProcedureFactory
    {
        public ProcedureFactory()
        {
            
        }

        public ScmProcedure Create(ScmExpression exp, ScmEnvironment env)
        {
            var variableName = (exp as VariableExpression).VariableName as string;
            return env.Dict[variableName] as ScmProcedure;
        }
    }
}
