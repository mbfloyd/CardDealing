using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class CardDealer : MonoBehaviour
{

    [SerializeField] private ScreenUI _screenUI;

    [SerializeField] private Transform dealingParent;
    [SerializeField] private Transform cardPoolParent;

    [SerializeField] private CardUI cardPrefab;

    [SerializeField] private GameObject deck;

    [SerializeField] private float timeBetweenDeals = 0.3f; 
    [SerializeField] private float dealDuration = 0.5f;
    [SerializeField] private float flipDuration = 0.5f;

    private IDataManager _dataManager;

    private List<CardUI> cardPool = new List<CardUI>();

    private List<CardUI> cardList = new List<CardUI>();

    public void Setup()
    {
        _dataManager = DIContainer.Instance.Resolve<IDataManager>();
        _dataManager.GetDeckService().OnCountChange += UpdateDeckCount;

        _screenUI.Setup();
        _screenUI.OnReset += Reset;
        _screenUI.OnDeal += Deal;
    }

    public void CleanUp()
    {
        _screenUI.OnReset -= Reset;
    }

    public void DealCards(int cardCount)
    {
        _screenUI.CreateSpots(cardCount);
        StopAllCoroutines();
        StartCoroutine(DealCardsCoroutine(cardCount));
    }
    private IEnumerator DealCardsCoroutine(int cardCount)
    {
        int dealtCards = 0;
        while (dealtCards < cardCount)
        {
            if (dealtCards > 0)
            {
                yield return new WaitForSeconds(timeBetweenDeals);
            }
            yield return DealCardToPlayer();

            yield return new WaitForSeconds(timeBetweenDeals);
            yield return DealCardToDealer();

            dealtCards++;
        }

    }

    private IEnumerator DealCardToPlayer()
    {
        Card card = _dataManager.GetDeckService().DealCard();
        SpotUI spot = _screenUI.GetNextPlayerSpot();
        yield return DealCard(card, spot);
    }

    private IEnumerator DealCardToDealer()
    {
        Card card = _dataManager.GetDeckService().DealCard();
        SpotUI spot = _screenUI.GetNextDealerSpot();
        yield return DealCard(card, spot);
    }

    private IEnumerator DealCard(Card card, SpotUI spot)
    {

        CardUI dealCard;
        if (cardPool.Count > 0)
        {
            dealCard = cardPool[0];
            dealCard.transform.SetParent(dealingParent);
            dealCard.RemovedFromPool();
            cardPool.RemoveAt(0);
        }
        else
        {
            dealCard = Instantiate<CardUI>(cardPrefab, dealingParent);
            dealCard.onRemove += CardRemoved;
        }
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

    private void UpdateDeckCount(int deckCount)
    {
        _screenUI.SetDeckCount(deckCount);
    }

    private void Reset()
    {
        _dataManager.ResetDealtCards();
        _dataManager.GetDeckService().ResetDeck();
        ResetTable();
    }

    private void ResetTable () {
        ClearCards();
        _screenUI.ClearSpots();
    }
    
    private void Deal(int cardCount)
{
    ResetTable();
    _dataManager.ResetDealtCards();
    if (_dataManager.CanDealCards(cardCount))
    {
        DealCards(cardCount);
        _dataManager.DealtCards(cardCount);
    }
    else
    {
        Debug.LogError("Can't deal " + cardCount + " cards.");
    }
}

    private void CardRemoved(CardUI card)
    {
        cardPool.Add(card);
        card.AddedToPool();
        card.transform.SetParent(cardPoolParent);
    }

    public void ClearCards()
    {
        int count = cardList.Count;
        for (int loop = 0; loop < cardList.Count; loop++)
        {
            CardUI card = cardList[loop];
            card.Remove();
        }
        cardList.Clear();
    }

}
