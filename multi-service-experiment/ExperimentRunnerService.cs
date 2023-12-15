using Experiment;
using Microsoft.Extensions.Hosting;
using Services.Storage;

namespace Services;

internal class ExperimentRunnerService : IHostedService
{
    public ExperimentRunnerService(IHostApplicationLifetime applicationLifetime,
        IExperimentStorage experimentStorage,
        ExperimentService experimentService)
    {
        this.applicationLifetime = applicationLifetime;
        this.experimentStorage = experimentStorage;
        this.experimentService = experimentService;
        onStartedRegistration = applicationLifetime.ApplicationStarted.Register(OnStarted);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        onStartedRegistration.Unregister();
        return Task.CompletedTask;
    }

    public void OnStarted()
    {
        Task.Run(async () =>
        {
            var performedExperiments = 0;
            var successCount = 0;

            while (performedExperiments < EXPERIMENTS_COUNT)
            {
                var experimentResult = await experimentService.PerformSingleExperiment();
                if (experimentResult == null)
                {
                    continue;
                }
                performedExperiments++;
                if (experimentResult.HasColorMatch)
                {
                    successCount++;
                }
                experimentStorage.StoreExperiment(new ExperimentData
                {
                    Cards = experimentResult.ShufledDeck,
                    HasCardMatch = experimentResult.HasColorMatch,
                });
            }

            Console.WriteLine($"Experiment results: {successCount} successful from {EXPERIMENTS_COUNT} cases.");
            Console.WriteLine($"Success rate: {(double)successCount / EXPERIMENTS_COUNT}");
            applicationLifetime.StopApplication();
        });
    }

    private const int EXPERIMENTS_COUNT = 1000;

    private IHostApplicationLifetime applicationLifetime;
    private IExperimentStorage experimentStorage;
    private ExperimentService experimentService;

    private CancellationTokenRegistration onStartedRegistration;
}
