﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
                
                if (value is string)
                {
                    int i;
                    double d;
                    //if (Int32.TryParse((string) value, out i))
                    //{
                    //    value = i;
                    //}
                    //else 
                    if (double.TryParse((string)value, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                    {
                        value = d;
                    }
                }
                _value = value;
            }
            
        }

        public override ScmExpression Eval(ScmEnvironment env)
        {
            return this;
        }

        public override string ToString()
        {
            if (_value is double)
            {
                return ((double) _value).ToString().Replace(',', '.');
            }
            else
            {
                return _value.ToString();
            }
        }
    }
}
