using System;
using System.Collections.Generic;


public interface IDeck
{
    public int Count();
    public void CreateNewDeck();
    public Card GetCard(int index);
    public void SetCard(int index, Card card);
    public Card DealCard();
    public void AddCards(List<Card> cards);
    
}
