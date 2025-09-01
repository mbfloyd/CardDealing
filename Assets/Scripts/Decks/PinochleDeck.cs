using System;
using System.Collections.Generic;

public class PinochleDecK : AbstractDeck, IDeck
{

    private  List<Rank> allowedRanks = new List<Rank>() { Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace};

    public void CreateNewDeck()
    {
        foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in allowedRanks)
            {
                //added twice for pinochle deck
                deck.Add(new Card(suit, rank));
                deck.Add(new Card(suit, rank));
            }
        }
    }

}
