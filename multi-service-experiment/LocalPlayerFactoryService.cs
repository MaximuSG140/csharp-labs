using Experiment;

namespace Services;

internal class LocalPlayerFactoryService : IPlayerFactoryService
{
    public LocalPlayerFactoryService(IPlayerStrategyFactoryService playerStrategyFactory)
    {
        this.playerStrategyFactory = playerStrategyFactory;
    }

    public IPlayer GetOrCreatePlayer(string name)
    {
        if (!players.ContainsKey(name))
        {
            players.Add(name,
                new LocalPlayer(playerStrategyFactory.GetStrategyForName(name)));
        }
        return players[name];
    }

    public IPlayer? GetPlayerIfExists(string name)
    {
        IPlayer? player;
        if (players.TryGetValue(name, out player))
        {
            return player;
        }
        return null;
    }

    public void RemovePlayer(string name)
    {
        players.Remove(name);
    }

    private IPlayerStrategyFactoryService playerStrategyFactory;
    private Dictionary<string, IPlayer> players = new();
}
