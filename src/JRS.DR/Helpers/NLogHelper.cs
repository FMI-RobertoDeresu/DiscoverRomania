using System.Linq;
using JRS.DR.Extensions;
using NLog;
using NLog.Targets;
using NLog.Web;

namespace JRS.DR.Helpers
{
    public static class NLogHelper
    {
        public static void ConfigureNLog(string connectionString, string nlogFile)
        {
            var configureNLog = NLogBuilder.ConfigureNLog(nlogFile);
            var dbTargets = configureNLog.Configuration.AllTargets
                .Where(x => x is DatabaseTarget)
                .Cast<DatabaseTarget>();

            dbTargets.ForEach(dbTarget => dbTarget.ConnectionString = connectionString);
            LogManager.ReconfigExistingLoggers();
        }
    }
}