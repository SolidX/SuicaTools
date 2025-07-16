using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solidus.SuicaTools.CardReaderCLI;
using Solidus.SuicaTools.Data;

internal partial class Program
{
    private static async Task Main(string[] args)
    {
        var host = Startup();
        await host.StartAsync();

        var cardReader = host.Services.GetService<ICardReaderService>();
        var readerInitialized = await cardReader.InitializeCardReader();

        host.Run();
    }

    static IConfigurationRoot BuildConfig(IConfigurationBuilder builder)
    {
        return builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
    }

    static IHost Startup()
    {
        var builder = new ConfigurationBuilder();
        var config = BuildConfig(builder);

        var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            services.AddDbContext<TransitContext>(options => options.UseSqlServer(config.GetConnectionString("TrainStations")));
            services.AddSingleton<ICardReaderService, SuicaCardReaderService>();
        }).Build();

        return host;
    }
}
