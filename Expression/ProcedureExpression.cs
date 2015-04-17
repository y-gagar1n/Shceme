using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    public class ProcedureExpression : ScmExpression
    {
        private ScmProcedure _proc;

        public ScmProcedure Proc
        {
            get { return _proc; }
            protected set { _proc = value; }
        }

        public ProcedureExpression(ScmProcedure proc)
        {
            _proc = proc;
        }

        protected override EvalResult EvalImpl(ScmEnvironment env)
        {
            throw new NotImplementedException();
        }
    }
}
