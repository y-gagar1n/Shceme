using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    public class SelfEvaluatingExpression : ScmExpression
    {
        private object _value;

        public object Value
        {
            get { return _value; }
        }

        public SelfEvaluatingExpression(object value)
        {
            if (value is SelfEvaluatingExpression)
            {
                _value = ((SelfEvaluatingExpression) value).Value;
            }
            else
            {
                _value = value;
            }
            
        }

        public override ScmExpression Eval(ScmEnvironment env)
        {
            return this;
        }
    }
}
