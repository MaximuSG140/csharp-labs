using Experiment;

namespace Services
{
    internal interface IDeckShufflerFactoryService
    {
        public abstract IDeckShuffler GetDeckShuffler();
    }
}