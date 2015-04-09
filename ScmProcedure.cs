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
        public bool IsPrimitive { get; set; }
        private string[] _parameters;

        public virtual string[] Parameters
        {
            get { return _parameters; }
        }

        public abstract object Apply(object[] args);
    }
}
