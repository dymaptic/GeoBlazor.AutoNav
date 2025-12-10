using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using dymaptic.GeoBlazor.AutoNav.Shared.Services;
using System.Reflection;
using dymaptic.GeoBlazor.Pro;

namespace dymaptic.GeoBlazor.AutoNav.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add device-specific services used by the dymaptic.GeoBlazor.AutoNav.Shared project
        builder.Services.AddSingleton<NavigationState>();
        builder.Services.AddSingleton<NavigationService>();

        builder.Services.AddMauiBlazorWebView();
        Assembly executingAssembly = Assembly.GetExecutingAssembly();

		using Stream stream = executingAssembly
			.GetManifestResourceStream(typeof(MauiProgram), "appsettings.json")!;

		IConfigurationRoot config = new ConfigurationBuilder()
			.AddJsonStream(stream)
			.Build();
		builder.Configuration.AddConfiguration(config);

		builder.Services.AddGeoBlazorPro(builder.Configuration);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
        
        using Stream devStream = executingAssembly
            .GetManifestResourceStream(typeof(MauiProgram), "appsettings.Development.json")!;

        config = new ConfigurationBuilder()
            .AddJsonStream(devStream)
            .Build();
        builder.Configuration.AddConfiguration(config);
#endif

        return builder.Build();
    }
}
