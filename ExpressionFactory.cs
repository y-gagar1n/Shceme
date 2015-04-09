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
            int i;
            //if (Int32.TryParse(text, out i))
            //{
            //    return new SelfEvaluatingExpression(i);
            //}
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

                if (pars[0].Value == "define")
                {
                    return new DefinitionExpression(pars.Skip(1).ToArray());
                }
                else
                {
                    var ve = new VariableExpression(pars[0].Value);
                    return new ApplicationExpression(ve, pars.Skip(1).Select(x => Create(x.Value)).ToArray());
                }
            }

            return new VariableExpression(text);
        }
    }
}
