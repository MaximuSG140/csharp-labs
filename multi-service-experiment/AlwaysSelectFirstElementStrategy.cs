using Experiment;

namespace Services;

internal class AlwaysSelectFirstElementStrategy : IPlayerStrategy
{
    public int ChooseCard(Deck cards)
    {
        return 0;
    }
}
