using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    class VoidExpression : SelfEvaluatingExpression
    {
        public VoidExpression() : base("OK")
        {
        }
    }
}
