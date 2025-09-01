using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenUI : MonoBehaviour
{

    public Transform dealingParent;
    public GameObject deck;
    public Button dealButton;
    public Button resetButton;
    public TMP_InputField cardCountInput;
    public TMP_Text deckCount;
    public CardUI cardPrefab;
    public GridHandUI gridHandUIprefab;
    public FanHandUI fanHandUIprefab;
    public Transform player;
    public Transform dealer;


    public Action OnReset;
    public Action<int> OnDeal;
    private List<CardUI> cardList = new List<CardUI>();
    private float dealDuration = 0.5f;
    private float flipDuration = 0.5f;
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

        playerHandUI.CreateSpots(cardCount);
        dealerHandUI.CreateSpots(cardCount);

        OnDeal?.Invoke(cardCount);
    }

    public IEnumerator DealCardToPlayer(Card card)
    {
        SpotUI spot = playerHandUI.GetNextAvailableSpot();
        yield return DealCard(card, spot);
    }

    public IEnumerator DealCardToDealer(Card card)
    {
        SpotUI spot = dealerHandUI.GetNextAvailableSpot();
        yield return DealCard(card, spot);
    }

    private IEnumerator DealCard(Card card, SpotUI spot)
    {
        CardUI dealCard = Instantiate<CardUI>(cardPrefab, dealingParent);
        dealCard.gameObject.SetActive(true);
        dealCard.transform.position = deck.transform.position;
        dealCard.SetCard(card);
        dealCard.ShowBack();
        cardList.Add(dealCard);

        yield return DealAndFlipCard(dealCard, spot);
    }

    private IEnumerator DealAndFlipCard(CardUI card, SpotUI spot)
    {
        Vector3 startWorldPos = card.transform.position;
        Vector3 targetWorldPos = spot.transform.position;

        Quaternion startWorldRot = card.transform.rotation;
        Quaternion targetWorldRot = spot.transform.rotation;

        float elapsed = 0f;
        while (elapsed < dealDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / dealDuration);

            // Lerp in world space
            Vector3 newWorldPos = Vector3.Lerp(startWorldPos, targetWorldPos, t);
            Quaternion newWorldRot = Quaternion.Lerp(startWorldRot, targetWorldRot, t);

            card.transform.position = newWorldPos;
            card.transform.rotation = newWorldRot;

            yield return null;
        }

        card.transform.position = targetWorldPos;
        card.transform.rotation = targetWorldRot;

        //flip card
        Quaternion startRotation = card.transform.rotation;
        Quaternion endRotation = card.transform.rotation;
        endRotation *= Quaternion.Euler(0f, 90f, 0f);

        elapsed = 0f;
        while (elapsed < dealDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / (flipDuration / 2));

            // Lerp in world space
            Quaternion newRotation = Quaternion.Lerp(startRotation, endRotation, t);
            card.transform.rotation = newRotation;

            yield return null;
        }
        card.ShowFront();

        startRotation = endRotation;
        endRotation *= Quaternion.Euler(0f, 90f, -1f * endRotation.eulerAngles.z);

        elapsed = 0f;
        while (elapsed < dealDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / (flipDuration / 2));

            // Lerp in world space
            Quaternion newRotation = Quaternion.Lerp(startRotation, endRotation, t);
            card.transform.rotation = newRotation;

            yield return null;
        }

        card.transform.SetParent(spot.transform);
    }

    public void ClearCards()
    {
        int count = cardList.Count;
        for (int loop = 0; loop < cardList.Count; loop++)
        {
            cardList[loop].Remove();
        }
        cardList.Clear();
        playerHandUI.Clear();
        dealerHandUI.Clear();
    }

    public void SetDeckCount(int count)
    {
        deckCount.text = count.ToString();
    }
    
}
