namespace Experiment;

public class RandomDeckShuffler : IDeckShuffler
{
    public Deck MakeShuffledCardDeck()
    {
        return new Deck(MakeFullDeck().OrderBy(card => new Random().Next()).ToArray());
    }

    static private Deck MakeFullDeck()
    {
        var suits = EnumValues<Card.SuitType>();
        var values = EnumValues<Card.ValueType>();
        var result = new Deck();
        foreach (var suit in suits)
        {
            foreach (var value in values)
            {
                result.Add(new Card { Suit = suit, Value = value });
            }
        }
        return result;
    }

    static private ICollection<EnumType> EnumValues<EnumType>() where EnumType : Enum
    {
        return Enum.GetValues(typeof(EnumType)).Cast<EnumType>().ToArray();
    }
}
