using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shceme.Expression;

namespace Shceme.Procedure
{
    public class PlusProcedure : ScmProcedure
    {
        public PlusProcedure(ScmExpression exp, ScmEnvironment env) : base(exp)
        {
        }

        public override object Apply(object[] args)
        {
            var see0 = args[0] as int?;
            var see1 = args[1] as int?;
            return see0 + see1;
        }
    }
}
