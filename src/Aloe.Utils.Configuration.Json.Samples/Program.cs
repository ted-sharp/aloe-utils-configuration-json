using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Aloe.Utils.Configuration.Json;

public static class AppConfig
{
    public static readonly List<string> ConfigFiles =
    [
        "appsettings.json",
        "appsettings.PostgreSQL.json",
        "appsettings.Serilog.json",
    ];

    public static IConfigurationRoot CreateConfigurationRoot()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFiles(ConfigFiles)
            .AddUserSecrets<Program>(optional: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}

public record AppSettings
{
    public bool IsStandalone { get; init; }
    public bool IsDebug { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
}

public class Program
{
    public static int Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        // 設定の追加
        builder.Configuration.AddConfiguration(AppConfig.CreateConfigurationRoot());

        // サービスの登録
        builder.Services.AddSingleton<App>();
        builder.Services.Configure<AppSettings>(
            builder.Configuration.GetSection("AppSettings"));

        var host = builder.Build();
        var app = host.Services.GetRequiredService<App>();
        return app.Run();
    }
}

public class App
{
    private readonly IConfiguration _configuration;
    private readonly AppSettings _appSettings;

    public App(IConfiguration configuration, IOptions<AppSettings> appSettings)
    {
        this._configuration = configuration;
        this._appSettings = appSettings.Value;
    }

    public int Run()
    {
        Console.WriteLine("アプリケーションの設定:");
        Console.WriteLine($"スタンドアロンモード: {this._appSettings.IsStandalone}");
        Console.WriteLine($"デバッグモード: {this._appSettings.IsDebug}");
        Console.WriteLine($"ユーザー名: {this._appSettings.Username}");
        Console.WriteLine($"パスワード: {this._appSettings.Password}");

        return 0;
    }
}
