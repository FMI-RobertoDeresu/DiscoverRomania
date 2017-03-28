using System.Collections.Generic;

namespace JRS.DR.WsModels
{
    public class ApiResponseBase
    {
        protected ApiResponseBase()
        {
            IsError = false;
            Messages = new List<string>();
        }

        public bool IsError { get; set; }

        public IList<string> Messages { get; private set; }
    }
}