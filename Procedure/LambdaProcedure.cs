﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shceme.Expression;

namespace Shceme.Procedure
{
    public class LambdaProcedure : ScmProcedure
    {
        private ScmExpression _scmExpression;
        private ScmEnvironment _scmEnvironment;
        private string[] _parameters;

        public override string[] Parameters
        {
            get { return _parameters; }
        }

        public LambdaProcedure(ScmExpression exp, ScmEnvironment env, string[] parameters)
        {
            _parameters = parameters;
            _scmEnvironment = env;
            _scmExpression = exp;
        }

        public override object Apply(object[] args)
        {
            Debug.Assert(args.Length >= _parameters.Length);
            var newEnv = _scmEnvironment.Extend();
            for (int i = 0; i < _parameters.Length; i++)
            {
                newEnv.Add(_parameters[i], args[i]);
            }
            var se = _scmExpression.Eval(newEnv).Value as SelfEvaluatingExpression;
            return se.Value;
        }
    }
}
