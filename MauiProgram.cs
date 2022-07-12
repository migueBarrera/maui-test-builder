using Microsoft.Extensions.Configuration;
using Serilog;
using System.Reflection;
using TestMaui.Features.Configuration;
using TestMaui.Features.NetworkClient;

namespace TestMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.AddSerilogFromConfiguration()
			.AddConfiguration()
			.ConfigureHttpClient()
			.ConfigureViewModels()
			.ConfigurePages()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}

    //Serilog
    //Serilog.Extensions.Logging
    //Serilog.Sinks.Console
    private static MauiAppBuilder AddSerilog(this MauiAppBuilder appBuilder)
	{
        Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.Enrich.FromLogContext()
			.WriteTo.Console()
			.CreateLogger();

        appBuilder.Logging.AddSerilog();

        return appBuilder;
	}
	
	//Microsoft.Extensions.Configuration.Binder
    //Microsoft.Extensions.Configuration.Json
    private static MauiAppBuilder AddConfiguration(this MauiAppBuilder appBuilder)
	{
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("TestMaui.appsettings.json");

        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

        appBuilder.Configuration.AddConfiguration(config);

		appBuilder.Services.AddTransient<ConfigurationService>();

        return appBuilder;
	}

    //Serilog
    //Serilog.Extensions.Logging
    //Serilog.Sinks.Console
    //Serilog.Settings.Configuration
    private static MauiAppBuilder AddSerilogFromConfiguration(this MauiAppBuilder appBuilder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(appBuilder.Configuration)
            .CreateLogger();

        appBuilder.Logging.AddSerilog();

        return appBuilder;
    }

    //Microsoft.Extensions.Http
    private static MauiAppBuilder ConfigureHttpClient(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddHttpClient("testConfig", config =>
		{
            config.BaseAddress = new Uri("https://www.google.com/");
		});
		
		////More configurations
		//appBuilder.Services.AddHttpClient("otherCongifuration", config =>
		//{
  //          config.BaseAddress = new Uri("https://www.youtube.com/");
		//});

		appBuilder.Services.AddTransient<TestNetworkService>();

		return appBuilder;
	}
	
	private static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddTransient<MainViewModel>();

		return appBuilder;
	}
	
	private static MauiAppBuilder ConfigurePages(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddTransient<MainPage>();

		return appBuilder;
	}
}
