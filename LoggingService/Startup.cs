using LoggingService.Core.BackgroundServices;
using LoggingService.Core.Core.Behaviors;
using LoggingService.Core.Handlers;
using LoggingService.Core.Interfaces;
using LoggingService.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace LoggingService
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
            services.AddEntityFrameworkNpgsql().AddDbContext<LoggingDbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("LoggingDbConnectionString")));

            services.AddMediatR(typeof(LoggingRequestHandler).GetTypeInfo().Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddScoped(typeof(ILoggingService), typeof(LoggingService));
            services.AddSingleton(typeof(ILogsQueue), typeof(LogsQueue));

            services.AddHostedService<TimedHostedService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Logging Service", Version = "v1", Contact = new OpenApiContact { Url = new Uri("https://proceedix.com"), Name = "Proceedix" } });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logging Service v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}