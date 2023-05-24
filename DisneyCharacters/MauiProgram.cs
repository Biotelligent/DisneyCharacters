namespace DisneyCharacters;
using DotNet.Meteor.HotReload.Plugin;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp()
    {
        DebugConstants.SanityCheck();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterAppServices()
            .RegisterViewModels()
#if DEBUG
            .EnableHotReload()
#endif
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });


        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<CharacterApiService>();
        return builder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<CharacterViewModel>();

        return builder;
    }
}