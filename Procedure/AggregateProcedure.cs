using System;
using System.Linq;

namespace Shceme.Procedure
{
    public class AggregateProcedure<T> : PrimitiveProcedure
    {
        private Func<T, int, T> _transformFirst;
        private readonly Func<T, T, T> _aggrFunc;
        private readonly T _seed;

        public AggregateProcedure(Func<T, T, T> aggrFunc, T seed = default(T))
        {
            _seed = seed;
            _aggrFunc = aggrFunc;
        }

        public AggregateProcedure<T> TransformFirst(Func<T, T> func)
        {
            _transformFirst = (f, n) => func(f);
            return this;
        }

        public AggregateProcedure<T> TransformFirst(Func<T, int, T> func)
        {
            _transformFirst = func;
            return this;
        }

        public override object Apply(object[] args)
        {
            return
                args.Select((x, i) =>
                {
                    return i == 0 && _transformFirst != null ? _transformFirst((T) x, args.Count()) : x;
                })
                    .OfType<T>()
                    .Aggregate<T, T>(_seed, _aggrFunc);
        }

        public static AggregateProcedure<T> Create(Func<T, T, T> aggrFunc, T seed = default(T))
        {
            return new AggregateProcedure<T>(aggrFunc, seed);
        }
    }
}