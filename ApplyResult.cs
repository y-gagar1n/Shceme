using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shceme
{
    public class ApplyResult
    {
        public object Value { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public ApplyResult(object value)
        {
            Success = true;
            Value = value;
        }

        public ApplyResult(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }

        public static ApplyResult From(object value)
        {
            return new ApplyResult(value);
        }
    }
}
