using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using TemplateEngine.AspNetCore.Extensions;

namespace SettlementTracker
{
    public class Program
    {
        public static int Main(string[] args)
        {
            // setup a bootstrap logger to capture output until the real logger is configured
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Example app failed to start.");
                return ex.HResult;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        /// <summary>
        /// Workhorse method to create a default host builder, register the Serilog logger
        /// for dependency injection, and register Template Engine components for dependency
        /// injection.
        /// </summary>
        /// <param name="args">The command line arguments</param>
        /// <returns>An IHostBuilder instance</returns>
        /// <remarks>The Template Engine components that are registered are 1) TemplateLoader,
        /// 2) TemplateCache 3) TemplateEngineSettings 4) All classes that descend from the
        /// MasterPresenterBase class. Additionally, the template directory is set to the path
        /// provided in the TemplateEngineSettings configuration section or to the default path
        /// if no usable configuration is found.</remarks>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, serilogConfig) => serilogConfig.ReadFrom.Configuration(context.Configuration))
                .UseTemplateEngine()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}



//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
