// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SMCS.Services.Api.Brokers.DateTimes;
using SMCS.Services.Api.Brokers.Loggings;
using SMCS.Services.Api.Brokers.Storages;
using SMCS.Services.Api.Services.Foundations.Schools;
using SMCS.Services.Api.Services.Foundations.Students;

namespace SMCS.Services.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            AddBrokers(services);
            AddServices(services);

            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = "SMCS.Services.Api",
                    Version = "v1"
                };

                options.SwaggerDoc(
                    name: "v1",
                    info: openApiInfo);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint(
                        name: "/swagger/v1/swagger.json",
                        url: "SMCS.Services.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void AddBrokers(IServiceCollection services)
        {
            services.AddScoped<IDateTimeBroker, DateTimeBroker>();
            services.AddScoped<ILogger, Logger<LoggingBroker>>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();
            services.AddScoped<IStorageBroker, StorageBroker>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ISchoolService, SchoolService>();
        }
    }
}
