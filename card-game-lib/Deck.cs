using System.Collections;

namespace Experiment;

public class Deck : IEnumerable<Card>
{
    public Deck() {}

    public Deck(ICollection<Card> cards)
    {
        this.cards = cards.ToList();
    }

    public Deck FirstHalf()
    {
        return new Deck(cards.Take(cards.Count / 2).ToArray());
    }

    public Deck SecondHalf()
    {
        return new Deck(cards.TakeLast(cards.Count / 2).ToArray());
    }

    public Card GetCardAt(int index)
    {
        return cards[index];
    }
    public void SetCardAt(int index, Card card)
    {
        cards[index] = card;
    }
    public void Add(Card card)
    {
        cards.Add(card);
    }
    public void RemoveAt(int index)
    {
        cards.RemoveAt(index);
    }
    public int Count
    {
        get
        {
            return cards.Count;
        }
    }
    public Card this[int i]
    {
        get
        {
            return GetCardAt(i);
        }
        set
        {
            SetCardAt(i, value);
        }
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (!(obj is Deck))
        {
            return false;
        }
        var deck_obj = (Deck)obj;
        return cards.SequenceEqual(deck_obj.cards);
    }

    private readonly List<Card> cards = new();
}
