using Experiment;
using Moq;

namespace lib_experiment_test;

[TestClass]
public class ExperimentUnitTest
{
    [TestMethod]
    public async void PerformExperimentSuccessfullTest()
    {
        MockPlayerSelection(mockFirstPlayer, FIRST_PLAYER_CARDS, 0);
        MockPlayerSelection(mockSecondPlayer, SECOND_PLAYER_CARDS, 0);
        MockShufflerReturnValue();
        
        var experiment = new Experiment.Experiment(mockFirstPlayer.Object,
                                                   mockSecondPlayer.Object,
                                                   mockCardShuffler.Object);
        var experimentResultTask = experiment.Perform();
        await experimentResultTask;
        Assert.IsNotNull(experimentResultTask.Result);
        Assert.IsTrue(experimentResultTask.Result.HasColorMatch);

        VerifyCardsPassedToPlayer(mockFirstPlayer, FIRST_PLAYER_CARDS);
        VerifyCardsPassedToPlayer(mockSecondPlayer, SECOND_PLAYER_CARDS);
    }

    [TestMethod]
    public async void PerformExperimentFailedTest()
    {
        MockPlayerSelection(mockFirstPlayer, FIRST_PLAYER_CARDS, 0);
        MockPlayerSelection(mockSecondPlayer, SECOND_PLAYER_CARDS, 1);
        MockShufflerReturnValue();

        var experiment = new Experiment.Experiment(mockFirstPlayer.Object,
                                                   mockSecondPlayer.Object,
                                                   mockCardShuffler.Object);
        var experimentResultTask = experiment.Perform();
        await experimentResultTask;
        Assert.IsNotNull(experimentResultTask.Result);
        Assert.IsFalse(experimentResultTask.Result.HasColorMatch);

        VerifyCardsPassedToPlayer(mockFirstPlayer, FIRST_PLAYER_CARDS);
        VerifyCardsPassedToPlayer(mockSecondPlayer, SECOND_PLAYER_CARDS);
    }

    private void MockPlayerSelection(Mock<IPlayer> player,
                                     ICollection<Card> playerCards,
                                     int selection)
    {
        player.Setup(p => p.ChooseCard(PassedCardsMatcher(playerCards)))
            .Returns(Task.FromResult<int?>(selection));
    }

    private void MockShufflerReturnValue()
    {
        mockCardShuffler.Setup(s => s.MakeShuffledCardDeck())
            .Returns(new Deck(FIRST_PLAYER_CARDS.Concat(SECOND_PLAYER_CARDS).ToArray()));
    }

    private void VerifyCardsPassedToPlayer(Mock<IPlayer> player, ICollection<Card> cards)
    {
        player.Verify(p => p.ChooseCard(PassedCardsMatcher(cards)), Times.Once());
    }

    private Deck PassedCardsMatcher(ICollection<Card> passedCards)
    {
        return It.Is<Deck>(cards =>
            cards.Intersect(passedCards).Count() == passedCards.Count());
    }

    private readonly Card[] FIRST_PLAYER_CARDS =
        {
            new Card() { Suit = Card.SuitType.Hearts, Value = Card.ValueType.Six },
            new Card() { Suit = Card.SuitType.Spades, Value = Card.ValueType.Six } 
        };
    private readonly Card[] SECOND_PLAYER_CARDS =
        {
            new Card() { Suit = Card.SuitType.Diamonds, Value = Card.ValueType.Ace },
            new Card() { Suit = Card.SuitType.Clubs, Value = Card.ValueType.Ace }
        };

    private Mock<IPlayer> mockFirstPlayer = new Mock<IPlayer>();
    private Mock<IPlayer> mockSecondPlayer = new Mock<IPlayer>();
    private Mock<IDeckShuffler> mockCardShuffler = new Mock<IDeckShuffler>();
}