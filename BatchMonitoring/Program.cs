using Serilog;
using BatchMonitoring.Services;
using TemplateEngine.AspNetCore.Extensions;

// setup a bootstrap logger to capture output until the real logger is configured
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var services = builder.Services;
    services.AddControllers();
    services.AddTemplateEngine();
    services.AddSingleton<IDataService, DataService>();

    builder.Host.UseSerilog((context, serilogConfig) => serilogConfig.ReadFrom.Configuration(context.Configuration));

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

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
