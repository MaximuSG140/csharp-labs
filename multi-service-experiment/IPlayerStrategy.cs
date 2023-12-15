using Experiment;

namespace Services
{
    internal interface IPlayerStrategy
    {
        public abstract int ChooseCard(Deck cards);
    }
}
