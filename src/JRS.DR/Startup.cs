using System;
using AutoMapper;
using JRS.DR.Contracts;
using JRS.DR.DbContexts;
using JRS.DR.Extensions;
using JRS.DR.Filters;
using JRS.DR.Helpers;
using JRS.DR.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace JRS.DR
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
                builder.AddUserSecrets("JRSDRSecrets");

            Configuration = builder.Build();
            NLogHelper.ConfigureNLog(Configuration["db:default"], "nlog.config");
        }

        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                //entity framework
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["db:default"]));

                //mvc
                services.AddMvc(
                        options =>
                        {
                            if (bool.Parse(Configuration["config:requireHttpsFilterEnabled"]))
                                options.Filters.Add(new RequireHttpsAttribute { Permanent = true });

                            options.Filters.Add(typeof(ApplicationExceptionFilter));

                            if (bool.Parse(Configuration["config:historyLogsEnabled"]))
                                options.Filters.Add(typeof(RequestHistoryLogFilterAttribute));
                        })
                    .AddJsonOptions(
                        options =>
                        {
                            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                            options.SerializerSettings.Formatting = Formatting.Indented;
                            options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        });

                //swagger
                services.AddSwaggerGen(
                    options =>
                    {
                        options.DescribeAllEnumsAsStrings();
                        options.DescribeStringEnumsInCamelCase();
                        options.SwaggerDoc("v1", new Info
                        {
                            Title = "JRS Discover Romania - API",
                            Version = "v1",
                            Contact = new Contact {Name = "JRS Discover Romania Team"},
                            Description = "JRS Discover Romania API methods."
                        });
                    });

                //automapper
                services.AddAutoMapper(
                    options =>
                    {
                        options.CreateMissingTypeMaps = true;
                        options.AllowNullCollections = true;
                    });

                //session
                services.AddDistributedMemoryCache();
                services.AddSession();

                //config
                services.AddSingleton(Configuration);

                //services
                services.RegisterServices();

                //repositories
                services.RegisterRepositories();

                //loggers
                services.RegisterLoggers();
            }
            catch (Exception exception)
            {
                new ApplicationLogger().LogError(exception);
                throw;
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLogger logger)
        {
            try
            {
                //env options
                if (env.IsDevelopment())
                    app.UseDeveloperExceptionPage();
                else
                    app.UseExceptionHandler("/error");

                //status code pages
                app.UseStatusCodePagesWithReExecute("/error/code/{0}");

                //static files
                app.UseStaticFiles();

                //session
                app.UseSession();

                //mvc
                app.UseMvc();

                //swagger
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
                    {
                        options.RoutePrefix = "api-docs";
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                    });

            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                throw;
            }
        }
    }
}