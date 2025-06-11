using System;
using UnityEngine;
public class CombatEntityModel
{
    public class OnMChangedEventArgs : EventArgs
    {
        public CombatEntity CombatEntity;
    }
    public Grid<CombatEntity> Grid { get; private set; }
    public event EventHandler<OnMChangedEventArgs> OnGridChanged;

    public void InitNewGrid(Grid<CombatEntity> grid)
    {
        if (grid == null)
        {
            Debug.LogWarning("Grid argument is null");
            return;
        }

        if (Grid != null)
        {
            Grid.OnGridObjectValueChanged -= HandleGridObjectValueChanged;
        }

        Grid = grid;
        Grid.OnGridObjectValueChanged += HandleGridObjectValueChanged;
        //OnGridChanged?.Invoke(this, new OnMChangedEventArgs { Grid = Grid });
    }
    private void HandleGridObjectValueChanged(object sender, Grid<CombatEntity>.OnGridObjectValueChangedEventArgs e)
    {
        OnGridChanged?.Invoke(this, new OnMChangedEventArgs { CombatEntity = Grid.GetGridObjectValue(e.x, e.y) });
    }
}
