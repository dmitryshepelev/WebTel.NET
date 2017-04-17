using System;
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
using WebTelNET.Auth.Resources;
using WebTelNET.Auth.Services;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Libs.ExceptionResolvers;
using WebTelNET.CommonNET.Services;
using WebTelNET.Auth.Models;
using WebTelNET.Auth.Models.Libs;

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
            services.AddDbContext<AuthDbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("PostgresConnectionString")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
            services.AddMvc();

            bool identityProductionMode = Convert.ToBoolean(Configuration[$"{nameof(AppSettings)}:IdentityProductionMode"]);
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryScopes(IdentityServerConfig.GetScopes())
                .AddInMemoryClients(IdentityServerConfig.GetClients(identityProductionMode))
                .AddAspNetIdentity<IdentityUser>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

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
                settings.LoginRedirect = Configuration[$"{appSettings}:LoginRedirect"];

                var databaseSettings = nameof(DatabaseSettings);
                var roleSettings = nameof(RoleSettings);
                settings.DatabaseSettings = new DatabaseSettings
                {
                    RoleSettings = new RoleSettings
                    {
                        AdminRole = Configuration[$"{appSettings}:{databaseSettings}:{roleSettings}:AdminRole"],
                        UserRole = Configuration[$"{appSettings}:{databaseSettings}:{roleSettings}:UserRole"]
                    }
                };
            });

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
            app.UseIdentityServer();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });

            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute("spa-fallback", new {controller = "home", action = "index"});
            });

            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<AuthDbContext>().Database.Migrate();

                    var userManager = app.ApplicationServices.GetService<UserManager<IdentityUser>>();
                    var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
                    var appSettings = app.ApplicationServices.GetService<IOptions<AppSettings>>();

                    serviceScope.ServiceProvider.GetService<AuthDbContext>().EnsureSeedData(userManager, roleManager, appSettings.Value.DatabaseSettings);
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
