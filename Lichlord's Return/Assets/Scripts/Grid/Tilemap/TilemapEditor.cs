using System.Collections.Generic;
using UnityEngine;
public class TilemapEditor : MonoBehaviour
{
    [SerializeField] private bool newSave;
    [SerializeField] private string newSaveName;
    [SerializeField] private List<Tile> tilePalette;
    [SerializeField] private int TileInPaletteID;
    private TilemapModel tilemap;
    public void Init(TilemapModel tilemap, GridInputHandler gridInputHandler)
    {
        gridInputHandler.OnCellClicked += GridClickHandler_OnCellChosen;
        this.tilemap = tilemap;
    }
    private void GridClickHandler_OnCellChosen(object sender, GridInputHandler.CellCoords e)
    {
        tilemap.Grid.SetGridObjectValue(e.toX, e.toY, tilePalette[TileInPaletteID]);
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (TileInPaletteID < tilePalette.Count - 1)
                TileInPaletteID++;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            if (TileInPaletteID > 0)
                TileInPaletteID--;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            tilemap.InitNewGrid(SaveSystem.LoadMostRecentObject<Grid<Tile>>());
            Debug.Log("Tilemap loaded!");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (newSave)
                SaveSystem.SaveObject(tilemap.Grid);
            else
                SaveSystem.SaveObject(newSaveName, tilemap.Grid, newSave);
            Debug.Log("Tilemap saved!");
        }
    }
}
