public class MoveCommand : Command
{
    private int fromX, fromY, toX, toY;
    private Grid<CombatEntity> grid;
    public MoveCommand(int fromX, int fromY, int toX, int toY, Grid<CombatEntity> grid) 
    {
        this.fromX = fromX;
        this.fromY = fromY;
        this.toX = toX;
        this.toY = toY;
        this.grid = grid;
    }
    public override void Execute()
    {
        grid.SetGridObjectValue(toX, toY, grid.GetGridObjectValue(fromX, fromY));
    }
}
