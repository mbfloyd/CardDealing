using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenUI : MonoBehaviour
{

    public Button dealButton;
    public Button resetButton;
    public TMP_InputField cardCountInput;
    public TMP_Text deckCount;
    public GridHandUI gridHandUIprefab;
    public FanHandUI fanHandUIprefab;
    public Transform player;
    public Transform dealer;


    public Action OnReset;
    public Action<int> OnDeal;

    private IHandUI playerHandUI;
    private IHandUI dealerHandUI;


    public void Setup()
    {
        dealButton.onClick.AddListener(Deal);
        resetButton.onClick.AddListener(Reset);

        CreatePlayerHand();
        CreateDealerHand();

    }

    private void CreatePlayerHand()
    {
        playerHandUI = Instantiate<GridHandUI>(gridHandUIprefab, player);
    }

    private void CreateDealerHand()
    {
        dealerHandUI = Instantiate<FanHandUI>(fanHandUIprefab, dealer);
    }

    private void Reset()
    {
        OnReset?.Invoke();
    }

    private void Deal()
    {
        int cardCount = 0;
        bool success = int.TryParse(cardCountInput.text.Trim(), out cardCount);

        OnDeal?.Invoke(cardCount);
    }

    public void CreateSpots(int cardCount)
    {
        ClearSpots();
        playerHandUI.CreateSpots(cardCount);
        dealerHandUI.CreateSpots(cardCount);
    }

    public SpotUI GetNextPlayerSpot()
    {
        return playerHandUI.GetNextAvailableSpot();
    }

    public SpotUI GetNextDealerSpot()
    {
        return dealerHandUI.GetNextAvailableSpot();
    }

    public void ClearSpots()
    {
        playerHandUI.ClearSpots();
        dealerHandUI.ClearSpots();
    }

    public void SetDeckCount(int count)
    {
        deckCount.text = count.ToString();
    }

    
    
}
