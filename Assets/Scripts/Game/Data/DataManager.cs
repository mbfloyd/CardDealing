using UnityEngine;

public class DataManager
{
    private Deck _deck;
    private int maxDealtCards = 12;

    private int dealtCards = 0;

    public DataManager()
    {
        _deck = new Deck();
    }


    public Deck GetDeck()
    {
        return _deck;
    }

    public void DealtCards(int cards)
    {
        dealtCards += cards;
    }

    public void ResetDealtCards()
    {
        dealtCards = 0;
    }

    public bool CanDealCards(int cards)
    {
        return dealtCards + cards <= maxDealtCards;
    }
}
