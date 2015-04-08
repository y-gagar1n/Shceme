using System;
using System.Linq;

namespace Shceme.Procedure
{
    public class AggregateProcedure<T> : PrimitiveProcedure
    {
        private Func<T, T> _transformFirst;
        private readonly Func<T, T, T> _aggrFunc;
        private readonly T _seed;

        public AggregateProcedure(Func<T, T, T> aggrFunc, T seed = default(T))
        {
            _seed = seed;
            _aggrFunc = aggrFunc;
        }

        public AggregateProcedure<T> TransformFirst(Func<T, T> func)
        {
            _transformFirst = func;
            return this;
        }

        public override object Apply(object[] args)
        {
            return
                args.Select((x, i) => i == 0 && _transformFirst != null ? _transformFirst((T) x) : x)
                    .OfType<T>()
                    .Aggregate<T, T>(_seed, _aggrFunc);
        }

        public static AggregateProcedure<T> Create(Func<T, T, T> aggrFunc, T seed = default(T))
        {
            return new AggregateProcedure<T>(aggrFunc, seed);
        }
    }
}