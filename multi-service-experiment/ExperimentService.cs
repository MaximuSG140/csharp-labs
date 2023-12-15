using Experiment;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

internal class ExperimentService
{
    const string FIRST_PLAYER_NAME = "Elon Musk";
    const string SECOND_PLAYER_NAME = "Mark Zuckerberg";

    public ExperimentService(IPlayerFactoryService playerFactoryService,
                             IDeckShufflerFactoryService deckShufflerFactoryService,
                             IExperimentStorage experimentStorage)
    {
        this.playerFactoryService = playerFactoryService;
        this.deckShufflerFactoryService = deckShufflerFactoryService;
        this.experimentStorage = experimentStorage;
    }

    public Task<ExperimentResult?> PerformSingleExperiment()
    {
        var experiment = new Experiment.Experiment(playerFactoryService.GetOrCreatePlayer(FIRST_PLAYER_NAME),
            playerFactoryService.GetOrCreatePlayer(SECOND_PLAYER_NAME), deckShufflerFactoryService.GetDeckShuffler());
        return experiment.Perform();
    }

    private readonly IPlayerFactoryService playerFactoryService;
    private readonly IDeckShufflerFactoryService deckShufflerFactoryService;
    private readonly IExperimentStorage experimentStorage;
}
