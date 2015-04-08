using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Hosting;
using System.Text;
using System.Threading.Tasks;

namespace Shceme
{
    public abstract class ScmProcedure
    {
        private ScmExpression _exp;
        //private ScmEnvironment _env;
        public bool IsPrimitive { get; set; }
        private string[] _parameters;

        public virtual string[] Parameters
        {
            get { return _parameters; }
        }

        public ScmProcedure(ScmExpression exp)
        {
            ///_env = env;
            _exp = exp;
        }

        public abstract object Apply(object[] args);
    }

    public class Arguments
    {
        private ScmExpression[] _exps;

        public Arguments(ScmExpression[] exps)
        {
            _exps = exps;
        }
    }
}
