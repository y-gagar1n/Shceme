using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shceme.Expression;

namespace Shceme.Procedure
{
    public class PlusProcedure : PrimitiveProcedure
    {
        public PlusProcedure()
        {
        }

        public override object Apply(object[] args)
        {
            return args.OfType<int>().Aggregate(0, (x, acc) => acc + x);
        }
    }
}
