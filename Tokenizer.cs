using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme
{
    public class Tokenizer
    {
        public IEnumerable<Token> Parse(string text)
        {
            var result = new List<Token>();
            string current = "";
            bool isToken = false;
            bool isTuple = false;
            int bracketsLvl = 0;
                
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case ' ':
                        if (isToken && bracketsLvl == 0)
                        {
                            result.Add(new Token(current, TokenType.Symbol));
                            current = "";
                            isToken = false;
                        }
                        if (bracketsLvl > 0)
                        {
                            current += text[i];
                        }
                        break;
                    case '(':
                        bracketsLvl++;
                        current += text[i];
                        isToken = true;
                        isTuple = true;
                        break;
                    case ')':
                        bracketsLvl--;
                        current += text[i];
                        if (bracketsLvl == 0)
                        {
                            result.Add(new Token(current, TokenType.Tuple));
                            current = "";
                            isToken = false;
                            isTuple = false;
                        }
                        break;
                    default:
                        current += text[i];
                        isToken = true;
                        break;
                }
            }

            if (isToken)
            {
                result.Add(new Token(current, isTuple ? TokenType.Tuple : TokenType.Symbol));
            }

            return result;
        }

        public string Strip(string input)
        {
            if (input.StartsWith("(") && input.EndsWith(")"))
            {
                input = input.Substring(1, input.Length - 2);
            }

            return input;
        }
    }
}
