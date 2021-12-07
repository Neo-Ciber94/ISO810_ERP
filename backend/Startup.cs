// #define MEMORY_DB
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotenv.net;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories;
using ISO810_ERP.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ISO810_ERP
{
    public class Startup
    {
        const string CorsPolicy = "Everyone";
        private static readonly bool EnableInMemoryDatabase = Environment.GetEnvironmentVariable("ISO810_ENABLE_IN_MEMORY_DB") == "true";
        private static readonly bool EnableHttpsRedirection = Environment.GetEnvironmentVariable("ISO810_ENABLE_HTTPS") == "true";

        static Startup()
        {
            // Read the .env file from the root of the project
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "../.env" }));
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            services.AddControllers();

            services.AddDbContext<ErpDbContext>(opt =>
            {
                if (EnableInMemoryDatabase)
                {
                    opt.UseInMemoryDatabase("ISO810_ERP");
                }
                else
                {
                    string? connectionString = Environment.GetEnvironmentVariable("ISO810_ERP_DB_CONNECTION_STRING");
                    if (connectionString == null)
                    {
                        throw new InvalidOperationException("ConnectionString is not set");
                    }

                    opt.UseSqlServer(connectionString);
                }
            });
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ISO810_ERP", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ISO810_ERP v1"));
            }

            app.UseRouting();

            app.UseCors(CorsPolicy);

            if (EnableHttpsRedirection)
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
