using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Procedure
{
    public class BinaryProcedure<TArg, TResult> : PrimitiveProcedure
    {
        private Func<TArg, TArg, TResult> _func;

        public BinaryProcedure(Func<TArg, TArg, TResult> func)
        {
            _func = func;
        }

        public override object Apply(object[] args)
        {
            return _func((TArg)args[0], (TArg)args[1]);
        }
    }
}
