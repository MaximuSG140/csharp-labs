using ConsoleExperiment;
using Experiment;

const int EXPERIMENTS_COUNT = 1_000;

var firstPlayer = new SimplePlayer();
var secondPlayer = new SimplePlayer();
var shuffler = new RandomDeckShuffler();

var performedExperiments = 0;
var successCount = 0;

while(performedExperiments < EXPERIMENTS_COUNT)
{
    var experiment = new Experiment.Experiment(firstPlayer,
                                               secondPlayer,
                                               shuffler);
    var result = await experiment.Perform();
    if (result == null)
    {
        continue;
    }
    performedExperiments++;
    if (result == Experiment.Experiment.Result.Success)
    {
        successCount++;
    }
}

Console.Out.WriteLine($"Experiment results: {successCount} successful from {EXPERIMENTS_COUNT} cases.");
Console.Out.WriteLine($"Success rate: {(double)successCount / EXPERIMENTS_COUNT}");
