using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme.Expression
{
    public class ConditionalExpression : ScmExpression
    {
        private Tokenizer _tokenizer = new Tokenizer();
        private ExpressionFactory _factory = new ExpressionFactory();
        private Token[] _clauses;

        public ConditionalExpression(Token[] clauses)
        {
            _clauses = clauses;
        }

        public override ScmExpression Eval(ScmEnvironment env)
        {
            if (!_clauses.Any())
            {
                return new SelfEvaluatingExpression(false);
            }
            else
            {
                foreach (var clause in _clauses)
                {
                    var tokens = _tokenizer.Parse(_tokenizer.Strip(clause.Value)).ToArray();
                    if (tokens[0].Value == "else")
                    {
                        return _factory.Create(tokens[1].Value).Eval(env);
                    }
                    else
                    {
                        var args = tokens.Select(x => _factory.Create(x.Value)).ToList();
                        if (True(args[0].Eval(env)))
                        {
                            return args[1].Eval(env);
                        }
                    }
                }
            }

            return new SelfEvaluatingExpression(false);
        }

        private bool True(ScmExpression exp)
        {
            var selfExp = exp as SelfEvaluatingExpression;
            return selfExp != null && selfExp.Value is bool && (bool)selfExp.Value;
        }
    }
}
