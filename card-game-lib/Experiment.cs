namespace Experiment;

public class Experiment
{
    public Experiment(IPlayer first, IPlayer second, IDeckShuffler shuffler)
    {
        firstPlayer = first;
        secondPlayer = second;
        cardShuffler = shuffler;
    }

    public async Task<ExperimentResult?> Perform()
    {
        var deck = cardShuffler.MakeShuffledCardDeck();
        var firstHalf = deck.FirstHalf();
        var firstPlayerSelection = await GetAndValidatePlayerSelection(firstPlayer, firstHalf);
        if (firstPlayerSelection == null)
        {
            return null;
        }

        var secondHalf = deck.SecondHalf();
        var secondPlayerSelection = await GetAndValidatePlayerSelection(secondPlayer, secondHalf);
        if (secondPlayerSelection == null) 
        { 
            return null;
        }

        return new()
        {
            ShufledDeck = deck,
            HasColorMatch = firstHalf[(int)secondPlayerSelection].HasSameColor(secondHalf[(int)firstPlayerSelection])
        };
    }

    static async Task<int?> GetAndValidatePlayerSelection(IPlayer player, Deck deck)
    {
        var selection = await player.ChooseCard(deck);
        if (selection == null)
        {
            return null;
        }
        if (selection >= deck.Count || selection < 0)
        {
            return null;
        }
        return selection;
    }

    private readonly IPlayer firstPlayer;
    private readonly IPlayer secondPlayer;
    private readonly IDeckShuffler cardShuffler;
}
