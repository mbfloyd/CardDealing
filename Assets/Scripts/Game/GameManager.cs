using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IDataManager _dataManager;
    [SerializeField] private CardDealer _cardDealer;

    void Awake()
    {
        Setup();
    }

    void Start()
    {
        StartGame();
    }

    void OnDestroy()
    {
        CleanUp();
    }

    private void Setup()
    {
        _dataManager = DIContainer.Instance.Resolve<IDataManager>();

        _cardDealer.Setup();
    }

    private void CleanUp()
    {
        _cardDealer.CleanUp();
    }

    public void StartGame()
    {
        //build fresh deck
        _dataManager.GetDeckService().CreateNewDeck();
    }

}