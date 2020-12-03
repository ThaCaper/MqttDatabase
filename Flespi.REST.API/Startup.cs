using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flespi.Core.AppService;
using Flespi.Core.AppService.Impl;
using Flespi.Core.DomainService;
using Flespi.Infrastructure.SQL;
using Flespi.Infrastructure.SQL.DbInitializer;
using Flespi.Infrastructure.SQL.Repositories;
using Flespi.REST.API.Extension;
using Flespi.REST.API.Mqtt;
using Flespi.REST.API.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.Extensions;

namespace Flespi.REST.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            Environment = env;

            MapConfiguration();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        private void MapConfiguration()
        {
            MapBrokerHostSettings();
            MapClientSettings();
        }

        private void MapBrokerHostSettings()
        {
            var brokerHostSettings = new BrokerHostSettings();
            Configuration.GetSection(nameof(BrokerHostSettings)).Bind(brokerHostSettings);
            AppSettingsProvider.BrokerHostSettings = brokerHostSettings;
        }

        private void MapClientSettings()
        {
            ClientSettings clientSettings = new ClientSettings();
            Configuration.GetSection(nameof(ClientSettings)).Bind(clientSettings);
            AppSettingsProvider.ClientSettings = clientSettings;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy("AllowSpecificOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            if (Environment.IsDevelopment())
                services.AddDbContext<DatabaseContext>(
                    opt => opt.UseSqlite("Data source=test.db"));

            if (Environment.IsProduction())
                services.AddDbContext<DatabaseContext>(
                    opt => opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddControllers();
            
            services.AddMqttClientHostedService();
            services.AddSingleton<ExternalService>();

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var service = app.ApplicationServices.CreateScope().ServiceProvider;
                var ctx = service.GetService<DatabaseContext>();
                var dbInitializer = service.GetService<IDbInitializer>();
                dbInitializer.Initialize(ctx);

                ctx.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }
            if(env.IsProduction())
            {
                var service = app.ApplicationServices.CreateScope().ServiceProvider;
                var ctx = service.GetService<DatabaseContext>();
                var dbInitializer = service.GetService<IDbInitializer>();
                dbInitializer.Initialize(ctx);

                ctx.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Forbindelse oprettet.");
                });
            });



        }
    }
}
