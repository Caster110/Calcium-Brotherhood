using System;
using UnityEngine;
public class TilemapModel
{
    public class OnMChangedEventArgs : EventArgs
    {
        public Grid<Tile> Grid;
    }
    public Grid<Tile> Grid { get; private set; }
    public event EventHandler<OnMChangedEventArgs> OnGridChanged;

    public void InitNewGrid(Grid<Tile> grid)
    {
        if (grid == null)
        {
            Debug.LogWarning("Grid argument is null");
            return;
        }

        if (Grid != null)
        {
            Grid.OnGridObjectValueChanged -= HandleGridChanged;
            Grid.OnGridArrayChanged -= HandleGridChanged;
        }

        Grid = grid;
        Grid.OnGridObjectValueChanged += HandleGridChanged;
        Grid.OnGridArrayChanged += HandleGridChanged;
        OnGridChanged?.Invoke(this, new OnMChangedEventArgs { Grid = Grid });
    }
    private void HandleGridChanged(object sender, EventArgs args)
    {
        OnGridChanged?.Invoke(this, new OnMChangedEventArgs { Grid = Grid });
    }
}
