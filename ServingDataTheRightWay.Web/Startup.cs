using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ServingDataTheRightWay.Data;
using ServingDataTheRightWay.Data.Models;

namespace ServingDataTheRightWay.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddDbContext<WideWorldImporters>(optionsAction =>
            {
                optionsAction.UseSqlServer(Configuration["ConnectionString:default"]);
                optionsAction.ConfigureWarnings(warningsAction =>
                {
                    warningsAction.Default(WarningBehavior.Log);
                    warningsAction.Ignore(RelationalEventId.BoolWithDefaultWarning);
                    warningsAction.Ignore(SqlServerEventId.DecimalTypeDefaultWarning);
                });
                optionsAction.EnableDetailedErrors();
            });

            services.AddTransient<DapperRepository>();
            services.AddTransient<AdoRepository>();
            services.AddTransient<EfCoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
