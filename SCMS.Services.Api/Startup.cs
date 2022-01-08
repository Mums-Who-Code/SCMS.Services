// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Services.Foundations.Guardians;
using SCMS.Services.Api.Services.Foundations.Schools;
using SCMS.Services.Api.Services.Foundations.StudentGuardians;
using SCMS.Services.Api.Services.Foundations.Students;
using SCMS.Services.Api.Services.Processings.StudentGuardians;
using SCMS.Services.Api.Services.Processings.Students;

namespace SCMS.Services.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddOData(options => options.Select().Filter().Expand().OrderBy());

            services.AddLogging();
            services.AddDbContext<StorageBroker>();
            AddBrokers(services);
            AddServices(services);

            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = "SCMS.Services.Api",
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
                        url: "SCMS.Services.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IStorageBroker, StorageBroker>();
        }

        private void AddServices(IServiceCollection services)
        {
            AddFoundationServices(services);
            AddProcessingServices(services);
        }

        private static void AddFoundationServices(IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ISchoolService, SchoolService>();
            services.AddTransient<IGuardianService, GuardianService>();
            services.AddTransient<IStudentGuardianService, StudentGuardianService>();
        }

        private static void AddProcessingServices(IServiceCollection services)
        {
            services.AddTransient<IStudentGuardianProcessingService, StudentGuardianProcessingService>();
            services.AddTransient<IStudentProcessingService, StudentProcessingService>();
        }
    }
}
