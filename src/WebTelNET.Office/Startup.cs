using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebTelNET.Office.Models;
using WebTelNET.Office.Models.Libs;
using WebTelNET.Office.Models.Repository;

namespace WebTelNET.Office
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
            services.AddDbContext<OfficeDbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("PostgresConnectionString")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<OfficeDbContext>()
                .AddDefaultTokenProviders();

            // Add framework services.
            services.AddMvc();

            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddScoped<IServiceStatusRepository, ServiceStatusRepository>();

            services.Configure<AppSettings>(settings =>
            {
                var appSettings = nameof(AppSettings);
                var serviceTypeNames = nameof(ServiceTypeNames);
                settings.ServiceTypeNames = new ServiceTypeNames
                {
                    PBXType = Configuration[$"{appSettings}:{serviceTypeNames}:PBXType"]
                };

                var serviceStatusNames = nameof(ServiceStatusNames);
                settings.ServiceStatusNames = new ServiceStatusNames
                {
                    Available = Configuration[$"{appSettings}:{serviceStatusNames}:Available"],
                    Activated = Configuration[$"{appSettings}:{serviceStatusNames}:Activated"],
                    Unavailable = Configuration[$"{appSettings}:{serviceStatusNames}:Unavailable"],
                };
            });
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

                ClientId = "office",
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

            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<OfficeDbContext>().Database.Migrate();

                    var serviceTypeRepository = app.ApplicationServices.GetService<IServiceTypeRepository>();
                    var serviceStatusRepository = app.ApplicationServices.GetService<IServiceStatusRepository>();
                    var appSettings = app.ApplicationServices.GetService<IOptions<AppSettings>>();

                    serviceScope.ServiceProvider.GetService<OfficeDbContext>().EnsureSeedData(serviceStatusRepository, serviceTypeRepository, appSettings.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The ERROR was occured during the database migration. -->");
                Console.WriteLine(e);
                Console.WriteLine("-- End Error --");
            }
        }
    }
}
