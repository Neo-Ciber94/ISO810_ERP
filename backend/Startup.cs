// #define MEMORY_DB
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using dotenv.net;
using ISO810_ERP.Config;
using ISO810_ERP.Extensions;
using ISO810_ERP.Filters;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories;
using ISO810_ERP.Repositories.Interfaces;
using ISO810_ERP.Services;
using ISO810_ERP.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ISO810_ERP
{
    public class Startup
    {
        const string CorsPolicy = "Everyone";
        private static readonly bool EnableInMemoryDatabase;
        private static readonly bool EnableHttpsRedirection;

        static Startup()
        {
            // Read the .env file from the root of the project
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "../.env" }));

            // Initialize the static variables
            EnableInMemoryDatabase = Environment.GetEnvironmentVariable("ISO810_ENABLE_IN_MEMORY_DB") == "true";
            EnableHttpsRedirection = Environment.GetEnvironmentVariable("ISO810_ENABLE_HTTPS") == "true";
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ISO810_ERP", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme in http cookies.",
                    Name = "Authorization",
                    In = ParameterLocation.Cookie,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

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

            services.AddDistributedMemoryCache();
            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<TypedCache>();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(options =>
            {
                options.Filters.Add<AppExceptionFilter>();
            });

            services.AddDbContext<ErpDbContext>(options =>
            {
                if (EnableInMemoryDatabase)
                {
                    options.UseInMemoryDatabase("ISO810_ERP");
                }
                else
                {
                    string? connectionString = Environment.GetEnvironmentVariable("ISO810_ERP_DB_CONNECTION_STRING");
                    if (connectionString == null)
                    {
                        throw new InvalidOperationException("ConnectionString is not set");
                    }

                    options.UseSqlServer(connectionString);
                }
            });

            services.AddJwtAuthentication();
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();
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

            app.UseWhen((_) => EnableHttpsRedirection, appBuilder =>
            {
                appBuilder.UseHttpsRedirection();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
