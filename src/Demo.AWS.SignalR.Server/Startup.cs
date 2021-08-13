using Demo.AWS.SignalR.Server.Hubs;
using Demo.AWS.SignalR.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Demo.AWS.SignalR.Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMetadataService, MetadataService>();

            services.AddCors();

            services
                .AddSignalR()
                .AddStackExchangeRedis(options => {
                    options.Configuration.ClientName = "DemoSignalR";
                    options.Configuration.EndPoints.Add(
                        this._configuration.GetConnectionString("Redis")
                    );
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(u =>
            {
                u.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                u.RoutePrefix = "swagger";
            });

            app.UseRouting();

            // Make sure the CORS middleware is ahead of SignalR.
            app.UseCors(builder =>
            {
                builder
                    .WithOrigins(
                        "http://localhost:5003"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();
            });
        }
    }
}