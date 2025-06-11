using System;

public class CombatInputHandler : IInputHandler
{
    private TilemapModel inputTilemap;
    private int lastClickedX;
    private int lastClickedY;
    private int lastCoveredX;
    private int lastCoveredY;
    public CombatHandler combatHandler;
    public CombatHandlerView combatView;
    public Character chosenCharacter;
    public Ability chosenAbility;
    private bool[,] availableMovement;
    private bool[,] route;
    public int MapSizeX => inputTilemap.Grid.Width;
    public int MapSizeY => inputTilemap.Grid.Height;
    public bool IsActive => chosenAbility != null;

    public void Init(TilemapModel tilemap, GridInputHandler gridInputHandler, CombatHandler combatHandler)
    {
        inputTilemap = tilemap;
        this.combatHandler = combatHandler;
        combatHandler.turnHandlerChanged += CombatHandler_turnHandlerChanged;
        combatHandler.abilityUsed += CombatHandler_abilityUsed;
        gridInputHandler.OnCellClicked += GridInputHandler_OnCellClicked;
        gridInputHandler.OnMouseMovedToCell += GridInputHandler_OnMouseMovedToCell;
    }

    private void CombatHandler_abilityUsed(object sender, CombatHandler.AbilityEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void CombatHandler_turnHandlerChanged(object sender, ITurnHandler turnHandler)
    {
        if (turnHandler is Character character)
        {
            chosenCharacter = character;
            chosenAbility = new MoveAbility(character);
            availableMovement = RouteFinder.FindReachable(chosenCharacter.X, chosenCharacter.Y,
                combatHandler.GetPassabilityMap(), chosenCharacter.MovementPoints);
            if (availableMovement[lastCoveredX, lastCoveredY])
            {
                route = RouteFinder.FindRoute(lastCoveredX, lastCoveredY,
                    chosenCharacter.X, chosenCharacter.Y, availableMovement);
            }
        }
    }

    private void GridInputHandler_OnMouseMovedToCell(object sender, GridInputHandler.CellCoords e)
    {
        lastCoveredX = e.toX;
        lastCoveredY = e.toY;
        if (chosenAbility is MoveAbility)
        {
            if (availableMovement[lastCoveredX, lastCoveredY])
            {
                route = RouteFinder.FindRoute(lastCoveredX, lastCoveredY, 
                    chosenCharacter.X, chosenCharacter.Y, availableMovement);
            }
        }
        else
        {
            availableMovement = new bool[MapSizeX, MapSizeY];
            route = new bool[MapSizeX, MapSizeY];
        }
    }

    private void GridInputHandler_OnCellClicked(object sender, GridInputHandler.CellCoords e)
    {
        if (chosenAbility != null)
        {
            combatHandler.AddToAbilityUsageQueue(chosenAbility.Use());
            chosenAbility = null;
        }
        else
        {
            //show info
            HighlightChosenTile(e.toX, e.toY);
        }
        int movementRadius = chosenCharacter.MovementPoints;
        int distance = Math.Abs(e.toX - movementRadius) + Math.Abs(e.toY - movementRadius);
        if (distance > movementRadius)
        {
            //return undraw or smth
        }
        int movementSquareSize = 2 * movementRadius + 1;
        bool[,] affordableMovement = new bool[movementSquareSize, movementSquareSize];
        for (int x = 0; x < movementSquareSize; x++)
        {
            for (int y = 0; y < movementSquareSize; y++)
            {
                distance = Math.Abs(x - movementRadius) + Math.Abs(y - movementRadius);
                affordableMovement[x, y] = distance <= movementRadius;
            }
        }

        route = RouteFinder.FindRoute(e.toX, e.toY, chosenCharacter.X, chosenCharacter.Y, combatHandler.GetPassabilityMap());

        int X = route.GetLength(0);
        int Y = route.GetLength(1);

        int characterMovementLeftBorderXOrigin = chosenCharacter.X - movementRadius;
        int characterMovementRightBorderXOrigin = chosenCharacter.X + movementRadius;
        int characterMovementLeftBorderYOrigin = chosenCharacter.Y - movementRadius;
        int characterMovementRightBorderYOrigin = chosenCharacter.Y + movementRadius;

        int characterMovementLeftBorderXOffset = Math.Max(characterMovementLeftBorderXOrigin, 0);
        int characterMovementRightBorderXOffset = Math.Min(characterMovementRightBorderXOrigin, X);
        int characterMovementLeftBorderYOffset = Math.Max(characterMovementLeftBorderYOrigin, 0);
        int characterMovementRightBorderYOffset = Math.Min(characterMovementRightBorderYOrigin, Y);

        bool[,] availableMovement = new bool[X, Y];

        for (int x = characterMovementLeftBorderXOffset; x < characterMovementRightBorderXOffset; x++)
        {
            for (int y = characterMovementLeftBorderYOffset; y < characterMovementRightBorderYOffset; y++)
            {
                availableMovement[x, y] = route[x, y] &&
                    affordableMovement[x - characterMovementLeftBorderXOrigin, y - characterMovementLeftBorderYOrigin];
            }
        }

        UpdateInputTilemap();
    }
    public void HighlightHoveredTile(int fromX, int fromY, int toX, int toY)
    {
        inputTilemap.Grid.SetGridObjectValue(toX, toY, new Tile(1));
        if (fromX == lastClickedX && fromY == lastClickedY)
        {
            inputTilemap.Grid.SetGridObjectValue(fromX, fromY, new Tile(2));
        }
        else
        {
            inputTilemap.Grid.SetGridObjectValue(fromX, fromY, new Tile(0));
        }
    }
    public void HighlightChosenTile(int newX, int newY)
    {
        inputTilemap.Grid.SetGridObjectValue(lastClickedX, lastClickedY, new Tile(0));
        lastClickedX = newX;
        lastClickedY = newY;
        inputTilemap.Grid.SetGridObjectValue(lastClickedX, lastClickedY, new Tile(2));
    }
    public void UpdateInputTilemap()
    {
        

        Tile[,] inputMatrix = new Tile[MapSizeX, MapSizeY];
        for (int x = 0; x < MapSizeX; x++)
        {
            for (int y = 0; y < MapSizeY; y++)
            {
                //priorities
                if (route[x, y])
                    inputMatrix[x, y] = new Tile(2);
                else if (availableMovement[x, y])
                    inputMatrix[x, y] = new Tile(1);
                else
                    inputMatrix[x, y] = new Tile(0);
            }
        }
        inputTilemap.Grid.SetGridArray(inputMatrix);
    }
    public bool TryDisable()
    {
        if(IsActive)
        {
            lastClickedX = -1; lastClickedY = -1;
            inputTilemap.Grid.SetGridArray(new Tile[0]);
            chosenAbility = null;
            return false;
        }
        else
        {
            //disable ui parts + input 
            return true;
        }
    }
}
