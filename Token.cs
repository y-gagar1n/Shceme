using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shceme
{
    public class Token
    {
        private string _value;

        public string Value
        {
            get { return _value; }
        }

        public TokenType Type
        {
            get { return _type; }
        }

        private TokenType _type;

        public Token(string value, TokenType type)
        {
            _type = type;
            _value = value;
        }
    }
}
