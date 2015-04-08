using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Procedure
{
    public class AggregateProcedure<T> : PrimitiveProcedure
    {
        private Func<T, T, T> _aggrFunc;
        private T _seed;

        public AggregateProcedure(Func<T, T, T> aggrFunc, T seed = default(T))
        {
            _seed = seed;
            _aggrFunc = aggrFunc;
        }

        public override object Apply(object[] args)
        {
            return args.OfType<T>().Aggregate<T,T>(_seed, _aggrFunc);
        }

        public static AggregateProcedure<T> Create(Func<T, T, T> aggrFunc, T seed = default(T))
        {
            return new AggregateProcedure<T>(aggrFunc, seed);
        }
    }
}
