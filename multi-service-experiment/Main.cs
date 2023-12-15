using Experiment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Storage;
using Services.Storage.SqlServer;

internal class Program
{
    private static void Main(string[] args)
    {
        MakeHostBuilder(args).Build().Run();
    }

    private static IHostBuilder MakeHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureServices(services =>
        {
            services.AddSingleton<IDeckShufflerFactoryService, ConstantDeckShufflerFactoryService>(
                (provider) => new ConstantDeckShufflerFactoryService(new RandomDeckShuffler()));
            services.AddSingleton<IPlayerStrategyFactoryService, ConstantPlayerStrategyFactoryService>(
                (provider) => new ConstantPlayerStrategyFactoryService(new AlwaysSelectFirstElementStrategy()));
            services.AddSingleton<IPlayerFactoryService, LocalPlayerFactoryService>();
            services.AddSingleton<IExperimentStorage, SqlServerExperimentStorage>();
            services.AddSingleton<ExperimentService>();
            services.AddHostedService<ExperimentRunnerService>();
        });
    }
}