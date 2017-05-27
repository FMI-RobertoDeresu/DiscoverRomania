using System;
using System.Collections.Generic;

namespace JRS.DR.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string error)
        {
            Errors = new List<string> { error };
        }

        public ApiException(IList<string> errors)
        {
            Errors = errors;
        }

        public IList<string> Errors { get; set; }
    }
}