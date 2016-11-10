using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTelNET.Auth.Resources;
using WebTelNET.Auth.Services;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Libs.ExceptionResolvers;
using WebTelNET.CommonNET.Services;
using WebTelNET.Models;
using WebTelNET.Models.Models;
using WebTelNET.Models.Repository;

namespace WebTelNET.Auth
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<WTIdentityDbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("PostgresConnectionString")));
            services.AddIdentity<WTUser, WTRole>()
                .AddEntityFrameworkStores<WTIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddMvc();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.SignIn.RequireConfirmedEmail = true;

                options.User.RequireUniqueEmail = true;
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

            services.AddScoped<SignInManager<WTUser>, WTSignInManager<WTUser>>();
            services.AddScoped<UserManager<WTUser>, WTUserManager<WTUser>>();
            services.AddScoped<IAccountResourceManager, AccountResourceManager>();
            services.AddScoped<IMailManager, MailManager>();
            services.AddScoped<IAuthMailCreator, AuthMailCreator>();
            services.AddScoped<IExceptionManager, ExceptionManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });

            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "home", action = "index" });
            });
        }
    }
}
