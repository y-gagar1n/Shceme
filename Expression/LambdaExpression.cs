﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shceme.Procedure;

namespace Shceme.Expression
{
    class LambdaExpression : ProcedureExpression
    {
        private static int nameCounter = 0;
        private string[] _parameters;
        private ScmExpression _body;
        private ScmEnvironment _env;

        public LambdaExpression(ScmExpression body, string[] parameters, ScmEnvironment env) : base(null)
        {
            _env = env;
            _body = body;
            _parameters = parameters;
        }

        protected override EvalResult EvalImpl(ScmEnvironment env)
        {
            var proc = new LambdaProcedure(_body, _env, _parameters);
            return (new ProcedureExpression(proc)).ToResult();
        }
    }
}
