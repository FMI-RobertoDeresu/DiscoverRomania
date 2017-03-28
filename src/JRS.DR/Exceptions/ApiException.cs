using System;
using System.Collections.Generic;

namespace JRS.DR.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(IList<string> errors)
        {
            Errors = errors;
        }

        public IList<string> Errors { get; set; }
    }
}