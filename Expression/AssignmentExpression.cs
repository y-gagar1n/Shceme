﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    public class AssignmentExpression : ScmExpression
    {
        private ScmExpression _value;
        private object _variable;

        public AssignmentExpression(object variable, ScmExpression value)
        {
            _variable = variable;
            _value = value;
        }

        protected override EvalResult EvalImpl(ScmEnvironment env)
        {
            SetVariable(_variable, _value.Eval(env).Value, env);
            return null;
        }

        private void SetVariable(object variable, ScmExpression eval, ScmEnvironment env)
        {
        }
    }
}
