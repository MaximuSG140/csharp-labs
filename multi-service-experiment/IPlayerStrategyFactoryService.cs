namespace Services;

internal interface IPlayerStrategyFactoryService
{
    public abstract IPlayerStrategy GetStrategyForName(string name);
}
