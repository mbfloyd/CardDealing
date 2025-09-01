using UnityEngine;

public class DataManager :IDataManager
{
    private DeckService deckService;

    private int maxDealtCards = 12;

    private int dealtCards = 0;

    public DataManager()
    {
        deckService = new DeckService();
    }

    public DeckService GetDeckService()
    {
        return deckService;
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
