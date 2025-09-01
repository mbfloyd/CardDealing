using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    void Awake()
    {

        // Register as Singleton (default is Singleton if not specified)
        DIContainer.Instance.Register<IDataManager, DataManager>(Lifetime.Singleton);

        //use standard 52 card deck
        DIContainer.Instance.Register<IDeck, Standard52CardDeck>();

        //use in a PinochleDecK
        //DIContainer.Instance.Register<IDeck, PinochleDecK>();


    }
}
