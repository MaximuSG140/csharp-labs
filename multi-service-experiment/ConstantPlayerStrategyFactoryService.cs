namespace Services;

internal class ConstantPlayerStrategyFactoryService : IPlayerStrategyFactoryService
{
    public ConstantPlayerStrategyFactoryService(IPlayerStrategy strategy)
    {
        this.strategy = strategy;
    }

    public IPlayerStrategy GetStrategyForName(string name)
    {
        return strategy;
    }

    private IPlayerStrategy strategy;
}
