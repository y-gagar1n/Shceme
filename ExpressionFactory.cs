using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shceme.Expression;

namespace Shceme
{
    public class ExpressionFactory
    {
        private readonly Tokenizer _tokenizer = new Tokenizer();

        public ScmExpression Create(string text)
        {
            text = _tokenizer.Strip(text, brackets:false);
            bool b;
            if (bool.TryParse(text, out b))
            {
                return new SelfEvaluatingExpression(b);
            }

            double d;
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
            {
                return new SelfEvaluatingExpression(d);
            }
            if(text.StartsWith("'") && text.EndsWith("'"))
            {
                return new SelfEvaluatingExpression(text.Replace("'", ""));
            }
            if (text.StartsWith("(") && text.EndsWith(")"))
            {
                var s = text.Substring(1, text.Length - 2);
                var pars = _tokenizer.Parse(s).ToArray();

                switch (pars[0].Value)
                {
                    case "define":
                        return new DefinitionExpression(pars.Skip(1).ToArray());
                    case "if":
                        var predicate = Create(pars[1].Value);
                        var then = Create(pars[2].Value);
                        var @else = Create(pars[3].Value);
                        return new IfExpression(predicate, then, @else);
                    case "cond":
                        return new ConditionalExpression(pars.Skip(1).ToArray());
                    default:
                        var ve = new VariableExpression(pars[0].Value);
                        return new ApplicationExpression(ve, pars.Skip(1).Select(x => Create(x.Value)).ToArray());
                }
            }

            return new VariableExpression(text);
        }
    }
}
