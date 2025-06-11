using System;
using UnityEngine;

[Serializable]
public class Grid<T>
{
    public class OnGridObjectValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
        public Grid<T> grid;
    }
    public class OnGridArrayChangedEventArgs : EventArgs
    {
        public readonly T[] gridArray;
        public Grid<T> grid;
        public OnGridArrayChangedEventArgs(T[] gridArray, Grid<T> grid)
        {
            this.gridArray = gridArray;
            this.grid = grid;
        }
    }
    public event EventHandler<OnGridObjectValueChangedEventArgs> OnGridObjectValueChanged;
    public event EventHandler<OnGridArrayChangedEventArgs> OnGridArrayChanged;
    public int Width => width;
    [SerializeField] private int width;
    public int Height => height;
    [SerializeField] private int height;

    [SerializeField] private T[] gridArray;

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridArray = new T[width * height];
        for (int x = 0; x < width * height; x++)
        {
            gridArray[x] = default;
        }
    }
    public Grid(int width, int height, T value)
    {
        this.width = width;
        this.height = height;

        gridArray = new T[width * height];
        for (int x = 0; x < width * height; x++)
        {
            gridArray[x] = value;
        }
    }
    public void SetGridArray(T[,] values)
    {
        int X = values.GetLength(0);
        int Y = values.GetLength(1);

        T[] result = new T[X * Y];

        for (int y = 0; y < X; y++)
        {
            for (int x = 0; x < Y; x++)
            {
                result[y * Y + x] = values[y, x];
            }
        }

        SetGridArray(result);
    }
    public void SetGridArray(T[] values)
    {
        if (values.Length == gridArray.Length)
        {
            gridArray = values;
            TriggerGridChanged();
        }
        else if (values.Length == 0)
        {
            gridArray = new T[width * height];
            for (int x = 0; x < width * height; x++)
            {
                gridArray[x] = default;
            }
            TriggerGridChanged();
        }
    }
    public void SetGridObjectValue(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            gridArray[x + y * width] = value;
            TriggerGridChanged(x, y);
        }
    }
    public T GetGridObjectValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            return gridArray[x + y * width];
        }
        else
        {
            return default;
        }
    }
    public void TriggerGridChanged(int x, int y)
    {
        OnGridObjectValueChanged?.Invoke(this, new OnGridObjectValueChangedEventArgs { x = x, y = y, grid = this});
    }
    public void TriggerGridChanged()
    {
        OnGridArrayChanged?.Invoke(this, new OnGridArrayChangedEventArgs(gridArray, this));
    }
}
