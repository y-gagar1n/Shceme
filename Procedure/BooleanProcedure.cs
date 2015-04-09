using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Procedure
{
    public class BooleanProcedure<T> : PrimitiveProcedure
    {
        private Func<T, T, bool> _func;

        public BooleanProcedure(Func<T, T, bool> func)
        {
            _func = func;
        }

        public override object Apply(object[] args)
        {
            return _func((T)args[0], (T)args[1]);
        }
    }
}
