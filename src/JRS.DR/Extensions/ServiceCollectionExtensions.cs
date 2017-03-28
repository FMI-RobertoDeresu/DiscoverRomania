using JRS.DR.Contracts;
using JRS.DR.Logging;
using JRS.DR.Repositories;
using JRS.DR.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JRS.DR.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IApiService, ApiService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<,>), typeof(RepositoryBase<,>));
            services.AddTransient<IObjectiveRepository, ObjectiveRepository>();
        }

        public static void RegisterLoggers(this IServiceCollection services)
        {
            services.AddTransient<IApplicationLogger, ApplicationLogger>();
            services.AddTransient<IRequestHistoryLogger, RequestHistoryLogger>();
        }
    }
}