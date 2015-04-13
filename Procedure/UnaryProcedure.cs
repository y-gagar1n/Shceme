using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Procedure
{
    public class UnaryProcedure<TArg, TResult> : PrimitiveProcedure
    {
        private Func<TArg, TResult> _func;

        public UnaryProcedure(Func<TArg, TResult> func)
        {
            _func = func;
        }

        public override object Apply(object[] args)
        {
            return _func((TArg)args[0]);
        }
    }
}
