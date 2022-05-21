using Hedgehog.Infrastructure;
using Hedgehog.UI.Infrastructure.Shared;
using NLog;
using Radzen;
using ILogger = NLog.ILogger;

namespace Hedgehog.UI;

public class Startup
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    public void ConfigureServices(IServiceCollection services)
    {
        Logger.Debug("Start {Method}", nameof(ConfigureServices));
        
        services.AddRazorPages(options => options.RootDirectory = "/Infrastructure/Pages");
        services.AddServerSideBlazor();
        services.AddSingleton<ThemeService>();
        services.AddScoped<ContextMenuService>();
        services.AddScoped<DialogService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<TooltipService>();

        Logger.Debug("End {Method}", nameof(ConfigureServices));
    }

    public void Configure(IApplicationBuilder app)
    {
        Logger.Debug("Start {Method}", nameof(Configure));

        if (Ambience.IsDevelopment == false)
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
        
        Logger.Debug("End {Method}", nameof(Configure));
    }
}