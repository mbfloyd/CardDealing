using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DataManager _dataManager;
    [SerializeField] private ScreenUI _screenUI;

    void Awake()
    {
        Setup();
    }

    void Start()
    {
        StartGame();
    }

    void Oestroy()
    {
        CleanUp();
    }

    private void Setup()
    {
        _dataManager = new DataManager();

        _screenUI.Setup();
        _screenUI.OnReset += Reset;
        _screenUI.OnDeal += Deal;

    }

    private void CleanUp()
    {
        _screenUI.OnReset -= Reset;
    }

    public void StartGame()
    {
        //build fresh deck
        _dataManager.GetDeck().CreateNewDeck();
        UpdateDeckCount();
    }

    private void Reset()
    {
        _dataManager.ResetDealtCards();
        _dataManager.GetDeck().ResetDeck();
        _screenUI.ClearCards();
        UpdateDeckCount();
    }

    protected void Deal(int cardCount)
    {
        _dataManager.ResetDealtCards();
        if (_dataManager.CanDealCards(cardCount))
        {
            StartCoroutine(DealCardsCoroutine(cardCount));
            _dataManager.DealtCards(cardCount);
        }
        else
        {
            Debug.LogError("Can't deal " + cardCount + " cards.");
        }
    }

    private IEnumerator DealCardsCoroutine(int cardCount)
    {
        int dealtCards = 0;
        float timeBetweenDeals = 0.3f; // wait 1/3 a second
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
        Card card = _dataManager.GetDeck().DealCard();
        UpdateDeckCount();
        yield return _screenUI.DealCardToPlayer(card);
    }

    private IEnumerator DealCardToDealer()
    {
        Card card = _dataManager.GetDeck().DealCard();
        UpdateDeckCount();
        yield return _screenUI.DealCardToDealer(card);
    }

    private void UpdateDeckCount()
    {
        int deckCount = _dataManager.GetDeck().CardsRemaining;
        _screenUI.SetDeckCount(deckCount);
    }

}