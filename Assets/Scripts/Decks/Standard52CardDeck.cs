using System;
using System.Collections.Generic;

public class Standard52CardDeck : AbstractDeck, IDeck
{

    public void CreateNewDeck()
    {
        deck.Clear();

        foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
            {
                Card card = new Card(suit, rank);
                deck.Add(card);
            }
        }
    }

}
