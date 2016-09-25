using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using AspNet.Security.OpenId;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace netcoretest
{
	public class Startup
    {
		public IConfiguration Configuration { get; set; }

		public Startup(IHostingEnvironment env)
		{
			Configuration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				//.AddEnvironmentVariables()
				.Build();

			// http://go.microsoft.com/fwlink/?LinkID=532709
			//var builder = new ConfigurationBuilder()
			//if (env.IsDevelopment())
			//{
			//	builder.AddUserSecrets()
			//}

			if (Configuration["SteamWebApiKey"] == null)
				throw new ArgumentException("SteamWebApiKey is not present in appsettings file.");
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
        {
			services.AddAuthentication(options => {
				options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			});

			services.AddAuthentication();
			services.AddAuthorization();
			//services.AddAuthorization(options =>
			//{
			//	options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
			//});

			services.AddMvc();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
				//app.UseDatabaseErrorPage();
				//app.UseBrowserLink();
			}

			app.Use(async (context, next) =>
			{
				await next();

				// In case of any 404 Exception we need to call the Index.html because the called URL could be a Angular2 route.
				// We need to ensure the SPA host (index.html) is called to handle that route:

				if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
				{
					context.Request.Path = "/index.html";
					await next();
				}
			});

			app.UseDefaultFiles();
			app.UseStaticFiles();
		
			// Serve /node_modules as a separate root (for packages that use other npm modules client side)

			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
				RequestPath = "/node_modules"
			}
			);

			//app.UseIdentity();

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				LoginPath = new PathString("/Account/Login"),
				//LogoutPath = new PathString("/Account/Logout"),
				Events = new CookieAuthenticationEvents()
				{
					OnValidatePrincipal = Controllers.AccountController.ValidatePrincipalAsync
				}
			});

			app.UseSteamAuthentication(new SteamAuthenticationOptions
			{
				ApplicationKey = this.Configuration["SteamWebAPIKey"],
				Events = new OpenIdAuthenticationEvents()
				{
					OnAuthenticated = (context) =>
					{
						return Task.FromResult(true);
					}
				}
			});
						
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
    }
}
