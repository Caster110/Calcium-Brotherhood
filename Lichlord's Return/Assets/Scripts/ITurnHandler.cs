public interface ITurnHandler
{
    public int Initiative { get; }
    public int NumberInQueue { get; set; }
    public void HandleTurn();
}
