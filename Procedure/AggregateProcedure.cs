using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Shceme.Procedure
{
    public class AggregateProcedure<T> : PrimitiveProcedure
    {
        private Func<T, int, T> _seedFunc;
        private readonly Func<T, T, T> _aggrFunc;

        public AggregateProcedure(Func<T, T, T> aggrFunc, Func<T, int, T> seedFunc = null)
        {
            _seedFunc = seedFunc ?? ((x, n) => x);
            _aggrFunc = aggrFunc;
        }
        
        public override ApplyResult Apply(object[] args)
        {
            var list = args
                .OfType<T>()
                .ToList();
            var seed = _seedFunc(list[0], list.Count);
            return ApplyResult.From(list.Skip(1).Aggregate(seed, _aggrFunc));
        }

        public static AggregateProcedure<T> Create(Func<T, T, T> aggrFunc, Func<T, int, T> seedFunc = null)
        {
            return new AggregateProcedure<T>(aggrFunc, seedFunc);
        }
    }
}