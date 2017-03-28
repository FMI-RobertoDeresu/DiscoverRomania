using System;

namespace JRS.DR.Contracts
{
    public interface IApplicationLogger
    {
        void LogError(Exception exception);
        void LogFatal(Exception exception);
    }
}