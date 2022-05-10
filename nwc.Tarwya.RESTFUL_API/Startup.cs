using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using nwc.Tarwya.Application.MapperProfiles;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Infra.Identity;
using nwc.Tarwya.Infra.Identity.Managers;
using nwc.Tarwya.Infra.Ioc;
using nwc.Tarwya.RESTFUL_API.Configurations;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace nwc.Tarwya.RESTFUL_API
{
    public class Startup
    {
        private string connectionString;
        private SystemSettings systemConfigurations = new SystemSettings();


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            connectionString = Configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);

            services.AddOptions();
            services.AddResponseCaching();
            services.Configure<SystemSettings>(Configuration.GetSection("SysSettings"));
            Configuration.Bind("SysSettings", systemConfigurations);
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://10.48.24.195:2041",
                                           "https://10.48.24.195:2031",
                                           "http://apretarapp001.nwc.com.sa:2041",
                                           "https://apretarapp001.nwc.com.sa:2031"
                                           );
                    });
            });
            services.AddHsts(options => {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
                options.IncludeSubDomains = true;
                options.Preload = true;
            });
            services.AddDbContext<TarwyaContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(connectionString);
            });
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser<long>, IdentityRole<long>>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api"));
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            AutoMapperConfig.RegisterMappings();

            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                {
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Authorization by Apikey inside request's header",
                    Scheme = "ApiKeyScheme"
                });

                var apiKeykey = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header
                };

                var requirement = new OpenApiSecurityRequirement
                {

                   { new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                            In = ParameterLocation.Header
                        }, new List<string>()
                   },
                };

                s.AddSecurityRequirement(requirement);


                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tarwya API Project",
                    Description = "Tarwya API Swagger surface"
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                ////... and tell Swagger to use those XML comments.
                //s.IncludeXmlComments(xmlPath);

            });
            //Locate the XML file being generated by ASP.NET...
            NativeInjectorBootStrapper.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            NLog.GlobalDiagnosticsContext.Set("DefaultConnection", connectionString);
            var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-SA"),
                    new CultureInfo("id-ID"),
                    new CultureInfo("fr-LU"),
                    new CultureInfo("fa-IR"),
                    new CultureInfo("ur-PK"),
                    new CultureInfo("tr-TR"),
                };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            };

            app.UseRequestLocalization(options);
            app.UseResponseCaching();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseCors();
            app.UseMvc();
            app.UseStaticFiles();
            app.UseHsts();
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("./swagger/v1/swagger.json", "Tarwya Project API v1.1");
                s.RoutePrefix = string.Empty;
            });
        }
    }
}
