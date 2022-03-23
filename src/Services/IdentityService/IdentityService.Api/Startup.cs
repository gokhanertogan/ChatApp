using IdentityService.Api.Extensions;
using IdentityService.Api.Helpers;
using IdentityService.Api.Infrastructure.Repositories;
using IdentityService.Api.Infrastructure.UnitOfWork;
using IdentityService.Api.Services.Abstract;
using IdentityService.Api.Services.Concrete;
using IdentityService.Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;

namespace IdentityService.Api
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
            services.Configure<GeneralConfigs>(Configuration.GetSection("GeneralConfigs"));
            services.AddSingleton<IGeneralConfigs>(sp =>
            {
                return sp.GetRequiredService<IOptions<GeneralConfigs>>().Value;
            });

            services.ConfigureDbContext(Configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRequestHelper, RequestHelper>();
            services.AddScoped<ISmsHelper, SmsHelper>();
            services.AddScoped<IOoredooClient, OoredooClient>();
            services.AddScoped<IPlatform, ChatWebPlatform>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityService.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
