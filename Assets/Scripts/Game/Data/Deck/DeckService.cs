using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckService
{
    private IDeck deck;

    public Action OnReset { get; set; }
    public Action<int> OnCountChange { get; set; }

    protected List<Card> dealtCards = new List<Card>(); // track dealt cards

    public DeckService()
    {
        deck = DIContainer.Instance.Resolve<IDeck>();
    }

    public void CreateNewDeck()
    {
        dealtCards.Clear();
        deck.CreateNewDeck();
        Shuffle();
        CountChange();
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();
        int index = deck.Count();
        while (index > 1)
        {
            index--;
            int randomIndex = random.Next(index + 1);
            Card temp = deck.GetCard(randomIndex);
            deck.SetCard(randomIndex, deck.GetCard(index));
            deck.SetCard(index, temp);
        }
    }

    public Card DealCard()
    {
        if (deck.Count() == 0)
        {
            ResetDeck();
        }

        Card card = deck.DealCard();
        dealtCards.Add(card);
        CountChange();
        return card;
    }

    public void ResetDeck()
    {
        deck.AddCards(dealtCards);
        dealtCards.Clear();
        Shuffle();
        CountChange();
        OnReset?.Invoke();
    }
    
    public int CardsRemaining() {
        return deck.Count();
    } 

    protected void CountChange()
    {
        OnCountChange?.Invoke(CardsRemaining());
    }
}
