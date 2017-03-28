using JRS.DR.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JRS.DR.Filters
{
    public class ApplicationExceptionFilter : IExceptionFilter
    {
        private readonly IApplicationLogger _appLogger;

        public ApplicationExceptionFilter(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }

        public void OnException(ExceptionContext context)
        {
            _appLogger.LogFatal(context.Exception);
        }
    }
}