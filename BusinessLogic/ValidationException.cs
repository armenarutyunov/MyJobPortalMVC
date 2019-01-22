using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ValidationException : Exception
    {
        public int Code { get; set; }
        public ValidationException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
