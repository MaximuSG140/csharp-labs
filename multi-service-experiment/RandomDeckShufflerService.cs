using Experiment;
using Microsoft.Extensions.Hosting;

namespace Services;

internal class ConstantDeckShufflerFactoryService : IDeckShufflerFactoryService
{
    public ConstantDeckShufflerFactoryService(IDeckShuffler deckShuffler)
    {
        this.deckShuffler = deckShuffler;
    }

    public IDeckShuffler GetDeckShuffler()
    {
        return deckShuffler;
    }

    private IDeckShuffler deckShuffler;
}
