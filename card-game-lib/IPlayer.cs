namespace Experiment;

public interface IPlayer
{
    public abstract Task<int?> ChooseCard(Deck cards);
}