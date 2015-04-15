using System.Collections.Generic;
using System.Linq;
using Shceme.Expression;

namespace Shceme
{
    public class ScmEnvironment
    {
        private Dictionary<string, object> _dict = new Dictionary<string, object>();

        public Dictionary<string, object> Dict
        {
            get { return _dict; }
        }

        public string[] Names
        {
            get { return _dict.Keys.ToArray(); }
        }

        public object[] Values
        {
            get { return _dict.Values.ToArray(); }
        }

        public void Add(string key, object value)
        {
            _dict.Add(key, value);
        }

        public ScmEnvironment Parent { get; private set; }

        public ScmEnvironment Extend()
        {
            return new ScmEnvironment {Parent = this};
        }

        public object Lookup(string variableName)
        {
            if (_dict.ContainsKey(variableName))
            {
                var value = _dict[variableName];
                if (value is VariableExpression)
                {
                    return Parent.Lookup((value as VariableExpression).VariableName);
                }
                else
                {
                    return value;
                }
            }
            else if (Parent != null)
            {
                return Parent.Lookup(variableName);
            }
            else return null;
        }
    }
}