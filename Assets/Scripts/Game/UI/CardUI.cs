using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class CardUI : MonoBehaviour, IPointerClickHandler
{
    public Action<CardUI> onRemove;

    public GameObject front;
    public GameObject back;

    public TMP_Text faceText;

    private Card card;

    private string poolNotation = "- Pool";


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(card + " clicked!");
    }

    public Card getCard()
    {
        return card;
    }

    public void SetCard(Card c)
    {
        card = c;
        UpdateFaceText();
    }

    public void ShowFront()
    {
        front.SetActive(true);
        back.SetActive(false);
    }

    public void ShowBack()
    {
        front.SetActive(false);
        back.SetActive(true);
    }

    public void Remove()
    {
        gameObject.SetActive(false);
        onRemove?.Invoke(this);
    }

    public void AddedToPool()
    {
        gameObject.name += poolNotation;
    }

    public void RemovedFromPool()
    {
        gameObject.name = gameObject.name.Replace(poolNotation, "");
    }
    
    private void UpdateFaceText()
    {
        Color color = Color.black;
        string suitChar = "";
        string rankChar = "";

        switch (card.suit)
        {
            case Suit.Clubs:
                color = Color.black;
                suitChar = "♣";
                break;
            case Suit.Diamonds:
                color = Color.red;
                suitChar = "♦";
                break;
            case Suit.Hearts:
                color = Color.red;
                suitChar = "♥";
                break;
            case Suit.Spades:
                color = Color.black;
                suitChar = "♠";
                break;
            default:
                break;
        }

        switch (card.rank)
        {
            case Rank.Two:
                rankChar = "2";
                break;
            case Rank.Three:
                rankChar = "3";
                break;
            case Rank.Four:
                rankChar = "4";
                break;
            case Rank.Five:
                rankChar = "5";
                break;
            case Rank.Six:
                rankChar = "6";
                break;
            case Rank.Seven:
                rankChar = "7";
                break;
            case Rank.Eight:
                rankChar = "8";
                break;
            case Rank.Nine:
                rankChar = "9";
                break;
            case Rank.Ten:
                rankChar = "10";
                break;
            case Rank.Jack:
                rankChar = "J";
                break;
            case Rank.Queen:
                rankChar = "Q";
                break;
            case Rank.King:
                rankChar = "K";
                break;
            case Rank.Ace:
                rankChar = "A";
                break;
            default:
                break;
        }

        faceText.color = color;
        faceText.text = rankChar + " " + suitChar;
    }
    
}
