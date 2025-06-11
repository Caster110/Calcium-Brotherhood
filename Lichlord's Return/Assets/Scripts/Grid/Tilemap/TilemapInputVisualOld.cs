
using UnityEngine;

public class TilemapInputVisualOld
{
    private TilemapModel inputTilemap;
    private int chosenX;
    private int chosenY;
    public TilemapInputVisualOld(TilemapModel tilemap, GridInputHandler gridInputHandler)
    {
        Debug.LogWarning("Old Version of Tilemap Vsual!");
        chosenX = -1;
        chosenY = -1;
        gridInputHandler.OnCellClicked += HighlightChosenTile;
        gridInputHandler.OnMouseMovedToCell += HighlightHoveredTile;
        inputTilemap = tilemap;
    }
    public void HighlightHoveredTile(object sender, GridInputHandler.CellCoords e)
    {
        inputTilemap.Grid.SetGridObjectValue(e.toX, e.toY, new Tile(1));
        if (e.fromX == chosenX && e.fromY == chosenY)
        {
            inputTilemap.Grid.SetGridObjectValue(e.fromX, e.fromY, new Tile(2));
        }
        else
        {
            inputTilemap.Grid.SetGridObjectValue(e.fromX, e.fromY, new Tile(0));
        }
    }
    public void HighlightChosenTile(object sender, GridInputHandler.CellCoords e)
    {
        inputTilemap.Grid.SetGridObjectValue(chosenX, chosenY, new Tile(0));
        chosenX = e.toX;
        chosenY = e.toY;
        inputTilemap.Grid.SetGridObjectValue(chosenX, chosenY, new Tile(2));
    }
}