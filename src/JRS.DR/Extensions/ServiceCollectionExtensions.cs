using JRS.DR.Contracts;
using JRS.DR.Logging;
using JRS.DR.Repositories;
using JRS.DR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JRS.DR.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IApiService, ApiService>();
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<,>), typeof(RepositoryBase<,>));
            services.AddTransient<IObjectiveRepository, ObjectiveRepository>();
            return services;
        }

        public static IServiceCollection RegisterLoggers(this IServiceCollection services)
        {
            services.AddTransient<IApplicationLogger, ApplicationLogger>();
            services.AddTransient<IRequestHistoryLogger, RequestHistoryLogger>();
            return services;
        }
    }
}