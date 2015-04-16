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
                if (char.IsWhiteSpace(text[i]))
                {
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
                }
                else
                {
                    switch (text[i])
                    {
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
            }

            if (isToken)
            {
                result.Add(new Token(current, isTuple ? TokenType.Tuple : TokenType.Symbol));
            }

            return result;
        }

        public string Strip(string input, bool brackets = true)
        {
            if (input.Length > 0)
            {
                int s = 0;
                int f = input.Length - 1;

                while (char.IsWhiteSpace(input[s])) s++;
                while (char.IsWhiteSpace(input[f]) && f > s) f--;

                if (brackets)
                {
                    if (input[s] == '(' && input[f] == ')')
                    {
                        s++;
                        f--;
                    }
                }

                input = input.Substring(s, f - s + 1);
            }

            return input;
        }
    }
}
