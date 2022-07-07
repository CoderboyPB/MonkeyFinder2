using MonkeyFinder2.Services;
using MonkeyFinder2.View;
using CommunityToolkit.Maui;

namespace MonkeyFinder2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>().UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FaBrandsRegular");
                fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FaFreeRegular");
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FaFreeSolid");
                fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesign");
            });

        builder.Services.AddSingleton<MonkeyService>();
        builder.Services.AddSingleton<MonkeysViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<HttpClient>();
        builder.Services.AddTransient<MonkeyDetailsViewModel>();
        builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        return builder.Build();
    }
}
