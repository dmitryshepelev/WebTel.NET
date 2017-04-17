using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Libs.Filters;
using WebTelNET.CommonNET.Services;
using WebTelNET.Office.Libs.Services;
using WebTelNET.PBX.Libs;
using WebTelNET.PBX.Models;
using WebTelNET.PBX.Models.Repository;
using WebTelNET.PBX.Services;

namespace WebTelNET.PBX
{
    public class Startup
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperConfiguration());
            });
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<PBXDbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("PostgresConnectionString")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<PBXDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(GlobalExceptionHandler));
            });

            services.Configure<AppSettings>(settings =>
            {
                var appSettings = nameof(AppSettings);
                var mailSettings = nameof(MailSettings);
                settings.MailSettings = new MailSettings
                {
                    LocalDomain = Configuration[$"{appSettings}:{mailSettings}:LocalDomain"],
                    SMTPServer = Configuration[$"{appSettings}:{mailSettings}:SMTPServer"],
                    Port = int.Parse(Configuration[$"{appSettings}:{mailSettings}:Port"]),
                    Login = Configuration[$"{appSettings}:{mailSettings}:Login"],
                    Password = Configuration[$"{appSettings}:{mailSettings}:Password"]
                };
            });

            services.AddScoped<ApiAuthorizeAttribute>();
            services.AddScoped<ClassConsoleLogActionOneFilter>();

            services.AddScoped<IMailManager, MailManager>();
            services.AddScoped<IMailCreator, PBXMailCreator>();
            services.AddScoped<IDispositionTypeRepository, DispositionTypeRepository>();
            services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
            services.AddScoped<IZadarmaAccountRepository, ZadarmaAccountRepository>();
            services.AddScoped<ICallRepository, CallRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IPBXManager, ZadarmaManager>();
            services.AddScoped<ICloudStorageService, YandexDiskClient>();
            services.AddScoped<IWidgetRepository, WidgetRepository>();
            services.AddScoped<IOfficeClient, OfficeClient>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMapper>(x => _mapperConfiguration.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentity();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });
            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = env.IsProduction() ? "http://auth.leadder.ru" : "http://localhost:5000",
                RequireHttpsMetadata = false,

                ClientId = "pbx",
                ClientSecret = "secret",

                ResponseType = "code id_token",
                Scope = {"api", "offline_access"},

                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true
            });

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });

            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute("spa-fallback", new {controller = "home", action = "index"});
            });
        }
    }
}
