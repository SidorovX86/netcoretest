using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace netcoretest
{
	public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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

			app.UseDefaultFiles();
			app.UseStaticFiles();

			//app.UseIdentity();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			//app.Use(async (context, next) =>
			//{
			//	await next();

			//	// In case of any 404 Exception we need to call the Index.html because the called URL could be a Angular2 route.
			//	// We need to ensure the SPA host (index.html) is called to handle that route:

			//	if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
			//	{
			//		context.Request.Path = "/index.html";
			//		await next();
			//	}
			//});
		}
    }
}
