using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeltaQrCode.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DeltaQrCode.Services;

namespace DeltaQrCode
{
    using AutoMapper;

    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.Repositories;
    using DeltaQrCode.Repositories.Guest;
    using DeltaQrCode.Repositories.Hotel_Positions;
    using DeltaQrCode.Repositories.SchimbAnvelope;
    using DeltaQrCode.Services.Guest;
    using DeltaQrCode.Services.Hotel;
    using DeltaQrCode.Services.Hotel_Positions;
    using DeltaQrCode.Services.Mail;
    using DeltaQrCode.Services.SchimbAnvelope;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection.Extensions;

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
            var EmailSettingsSection =
                Configuration.GetSection("EmailSettings");
            services.Configure<EmailSettings>(EmailSettingsSection);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // Register Services for injection
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            services.AddTransient<IHttpHelper, HttpHelper>();
            services.AddTransient<IQrService, QrService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IGuestService, GuestService>();
            services.AddTransient<ISchimbAnvelopeService, SchimbAnvelopeService>();
            services.AddTransient<IHotelPositionsService, HotelPositionsService>();

            services.AddTransient<IGuestRepository, GuestRepository>();
            services.AddTransient<IHotelAnvelopeRepository, HotelAnvelopeRepository>();
            services.AddTransient<IAppointmentsRepository, AppointmentsRepository>();
            services.AddTransient<ISchimbAnvelopeRepository, SchimbAnvelopeRepository>();
            services.AddTransient<IHotelPositionsRepository, HotelPositionsRepository>();

            // AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // DBContexts
            // for data
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            // for auth
            services.AddDbContext<AuthDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("AuthConnection")));

            //services.AddDataProtection().UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
            //{
            //    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
            //    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            //});

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
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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
