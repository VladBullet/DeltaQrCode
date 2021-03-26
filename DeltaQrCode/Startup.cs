﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeltaQrCode.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DeltaQrCode.Services;

namespace DeltaQrCode
{
    using DeltaQrCode.Repositories;
    using DeltaQrCode.Services.Hotel;

    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
    using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Register Services for injection
            services.AddScoped<IQrService, QrService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddScoped<IHotelAnvelopeRepository, HotelAnvelopeRepository>();
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();

            // DBContexts
            // for data
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            // for auth
            services.AddDbContext<AuthDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("AuthConnection")));

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<AuthDbContext>();
            services.AddDataProtection().UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Account/Login";
                    options.LogoutPath = $"/Account/Logout";
                    options.AccessDeniedPath = $"/Account/AccessDenied";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Strict
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
