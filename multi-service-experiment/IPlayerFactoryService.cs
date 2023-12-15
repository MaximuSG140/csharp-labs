namespace Services;

internal interface IPlayerFactoryService
{
    public abstract Experiment.IPlayer GetOrCreatePlayer(string name);
    public abstract Experiment.IPlayer? GetPlayerIfExists(string name);

    public abstract void RemovePlayer(string name);
}
