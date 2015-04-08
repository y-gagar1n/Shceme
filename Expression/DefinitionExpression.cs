using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    public class DefinitionExpression : ScmExpression
    {
        private ScmExpression _value;
        private Token[] _arguments;
        private Tokenizer _tokenizer = new Tokenizer();
        private ExpressionFactory _factory = new ExpressionFactory();

        public DefinitionExpression(Token[] arguments)
        {
            _arguments = arguments;
        }

        public override ScmExpression Eval(ScmEnvironment env)
        {
            if (_arguments[0].Type == TokenType.Symbol)
            {
                DefineVariable(_arguments[0].Value, new SelfEvaluatingExpression(_arguments[1].Value), env);
            }
            else if(_arguments[0].Type == TokenType.Tuple)
            {
                var pars = _tokenizer.Parse(_tokenizer.Strip(_arguments[0].Value)).ToList();
                var newVariable = pars[0].Value;
                var lambda = new LambdaExpression(_factory.Create(_arguments[1].Value), pars.Skip(1).Select(x => x.Value).ToArray(), env);
                var newValue = lambda.Eval(env);
                DefineVariable(newVariable, newValue, env);
            }

            return new VoidExpression();
        }

        private void DefineVariable(string var, ScmExpression val, ScmEnvironment env)
        {
            env.Dict[var] = val;
        }
    }
}