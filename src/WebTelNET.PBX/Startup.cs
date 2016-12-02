using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Libs.ExceptionResolvers;
using WebTelNET.CommonNET.Services;
using WebTelNET.Models;
using WebTelNET.Models.Libs;
using WebTelNET.Models.Models;
using WebTelNET.Models.Repository;
using WebTelNET.PBX.Libs;

namespace WebTelNET.PBX
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IZadarmaAccountRepository, ZadarmaAccountRepository>();
            services.AddScoped<UserManager<WTUser>, WTUserManager<WTUser>>();

            services.AddMvc();

            //services.AddScoped<ConsoleLogActionOneFilter>();
            services.AddScoped<ClassConsoleLogActionOneFilter>();
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

                Authority = "http://localhost:5000",
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
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "home", action = "index" });
            });
        }
    }
}
