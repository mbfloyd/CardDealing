using System;
using System.Collections.Generic;

public abstract class AbstractDeck
{

    protected List<Card> deck = new List<Card>();       // current deck pool

    public int Count()
    {
        return deck.Count;
    }

    public Card GetCard(int index)
    {
        return deck[index];
    }

    public void SetCard(int index, Card card)
    {
        deck[index] = card;
    }

    public Card DealCard()
    {
        Card card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    public void AddCards(List<Card> cards)
    {
         deck.AddRange(cards);
    }

}
