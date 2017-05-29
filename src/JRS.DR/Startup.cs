﻿using AutoMapper;
using JRS.DR.DbContexts;
using JRS.DR.Extensions;
using JRS.DR.Filters;
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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
                builder = builder.AddUserSecrets("jrsdrsecrets");

            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //entity framework
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["db:default"]));

            //mvc
            services.AddMvc(
                    options =>
                    {
                        if (bool.Parse(Configuration["app:requireHttpsFilterEnabled"]))
                            options.Filters.Add(new RequireHttpsAttribute { Permanent = true });

                        if (bool.Parse(Configuration["app:applicationExceptionFilterEnabled"]))
                            options.Filters.Add(typeof(ApplicationExceptionFilter));

                        if (bool.Parse(Configuration["app:requestHistoryLogFilterEnabled"]))
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
                        Contact = new Contact { Name = "JRS Discover Romania Team" },
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //logger
            app.ConfigureNLog(env, loggerFactory, Configuration["db:default"]);

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
            app.UseMvc(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));

            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "api-docs";
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                });
        }
    }
}