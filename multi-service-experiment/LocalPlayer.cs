using Experiment;

namespace Services;

// An implementation of player interface, which lives in
// the same process with ExperimentService.
internal class LocalPlayer : Experiment.IPlayer
{
    public LocalPlayer(IPlayerStrategy strategy)
    {
        this.strategy = strategy;
    }

    public Task<int?> ChooseCard(Deck cards)
    {
        return Task.FromResult<int?>(strategy.ChooseCard(cards));
    }

    private IPlayerStrategy strategy;
}
