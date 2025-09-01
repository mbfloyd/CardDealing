
public interface IDataManager
{
    public DeckService GetDeckService();
    public void DealtCards(int cards);
    public void ResetDealtCards();
    public bool CanDealCards(int cards);
    
}
