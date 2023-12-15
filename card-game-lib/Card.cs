namespace Experiment;

public struct Card
{
    public enum SuitType
    {
        Hearts, Spades, Diamonds, Clubs
    }

    public enum ValueType
    {
        Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }

    public SuitType Suit { get; set; }
    public ValueType Value { get; set; }

    public bool HasSameColor(Card other)
    {
        return IsBlack(Suit) == IsBlack(other.Suit);
    }

    public static bool IsBlack(SuitType type)
    {
        return type == SuitType.Hearts || type == SuitType.Diamonds;
    }
}
