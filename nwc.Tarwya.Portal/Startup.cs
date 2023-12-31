﻿using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using nwc.Tarwya.Application.Helpers;
using nwc.Tarwya.Application.MapperProfiles;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Infra.Identity;
using nwc.Tarwya.Infra.Identity.Managers;
using nwc.Tarwya.Infra.Ioc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal
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
        public IJobManager JobManager { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            connectionString = Configuration.GetConnectionString("DefaultConnection");

            Console.WriteLine(connectionString);
            services.Configure<SystemSettings>(Configuration.GetSection("SysSettings"));
            Configuration.Bind("SysSettings", systemConfigurations);

            services.AddMemoryCache();
            services.AddOptions();
            services.AddLocalization();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://10.68.141.97:2030",
                                           "http://10.48.24.195:2040",
                                           "https://10.48.24.195:2030",
                                           "http://apretarapp001.nwc.com.sa:2040",
                                           "https://apretarapp001.nwc.com.sa:2030"
                                           );
                    });
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
			services.AddDataProtection()
				.PersistKeysToFileSystem(new DirectoryInfo(systemConfigurations.DataProtectionKeyPath))
				.SetApplicationName("Tarwiah");

			services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.PostConfigure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme,
                opt =>
                {
                    //configure your other properties
                    opt.LoginPath = "/Account/login";
                });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

                options.AddPolicy("", p => { p.RequireClaim("", ""); });

            });
            services.AddHangfire(c =>
            {
                c.UseSqlServerStorage(connectionString);
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

            // Add framework services.
            services
                .AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
            // Maintain property names during serialization. See:
            // https://github.com/aspnet/Announcements/issues/194
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // Add Kendo UI services to the services container
            services.AddKendo();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            AutoMapperConfig.RegisterMappings();
            NativeInjectorBootStrapper.RegisterServices(services);

            //JobManager = services.BuildServiceProvider().GetService<IJobManager>();
            //JobStorage.Current = new SqlServerStorage(connectionString);
            //JobManager.StartProcess();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            NLog.GlobalDiagnosticsContext.Set("DefaultConnection", connectionString);
            NLog.GlobalDiagnosticsContext.Set("App", "Portal");
            var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG"),
                };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US", "en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
            };
            app.UseRequestLocalization(options);
            NLog.GlobalDiagnosticsContext.Set("DefaultConnection", connectionString);
            if (env.IsDevelopment() || env.IsEnvironment("UAT") || env.IsEnvironment("PREPROD"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());
            }

            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    await Task.Run(()=>response.Redirect("/Account/AccessDenied"));
            });
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCors();
            //var Hngfiretorage = new SqlServerStorage(connectionString);

            //app.UseHangfireDashboard("/SyncJobs", new DashboardOptions()
            //{
            //    DashboardTitle = "Tarwya Background Tasks",
            //    DisplayStorageConnectionString = false,

            //}, Hngfiretorage);

            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
