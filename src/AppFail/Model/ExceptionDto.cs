using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppfailReporting.Model
{
    public class ExceptionDto
    {
        public ExceptionDto(string stackTrace, string message, string type)
        {
            StackTrace = stackTrace;
            ExceptionMessage = message;
            ExceptionType = type;
        }

        public string StackTrace { get; private set; }
        public string ExceptionMessage { get; private set; }
        public string ExceptionType { get; private set; }
    }
}
