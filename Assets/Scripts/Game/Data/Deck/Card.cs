

[System.Serializable]
public class Card
{
    public Suit suit { get; private set; }
    public Rank rank { get; private set; }

    public Card(Suit s, Rank r)
    {
        suit = s;
        rank = r;
    }

    public override string ToString()
    {
        return rank +" of "+ suit;
    }
}