using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private List<Card> deck = new List<Card>();       // current deck pool
    private List<Card> dealtCards = new List<Card>(); // track dealt cards


    public void CreateNewDeck()
    {
        deck.Clear();
        dealtCards.Clear();

        foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
            {
                Card card = new Card(suit, rank);
                deck.Add(card);
            }
        }

        Shuffle();
    }

    public void Shuffle()
    {
        System.Random rng = new System.Random();
        int n = deck.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }

    public Card DealCard()
    {
        if (deck.Count == 0)
        {
            CreateNewDeck();
        }

        Card card = deck[0];
        deck.RemoveAt(0);
        dealtCards.Add(card);
        return card;
    }

    public void ResetDeck()
    {
        CreateNewDeck();
    }

    public int CardsRemaining => deck.Count;
}
