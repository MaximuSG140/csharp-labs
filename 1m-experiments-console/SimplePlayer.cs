using Experiment;

namespace ConsoleExperiment;
internal class SimplePlayer : Experiment.IPlayer
{
    Task<int?> IPlayer.ChooseCard(Deck cards)
    {
        return Task.FromResult((int?)0);
    }
}
