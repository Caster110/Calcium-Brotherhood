using System;

using UnityEngine;

[Serializable]
public class Grid<T>
{
    public event EventHandler<OnGridObjectValueChangedEventArgs> OnGridObjectValueChanged;
    //offset
    public int Width => width;
    [SerializeField] private int width;
    public int Height => height;
    [SerializeField] private int height;
    public float CellSize => cellSize;
    [SerializeField] private float cellSize;
    public Vector3 OriginPosition => originPosition;
    [SerializeField] private Vector3 originPosition;
    [SerializeField] private GridObject<T>[] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<int, int, GridObject<T>> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new GridObject<T>[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x + y * width] = createGridObject(x, y);
            }
        }
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / CellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / CellSize);
    }
    public void SetGridObjectValue(Vector3 worldPosition, T value)
    {
        GetXY(worldPosition, out int x, out int y);
        SetGridObjectValue(x, y, value);
    }

    public void SetGridObjectValue(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            gridArray[x + y * width].value = value;
            TriggerGridChanged(x, y);
        }
    }

    public void TriggerGridChanged(int x, int y)
    {
        OnGridObjectValueChanged?.Invoke(this, new OnGridObjectValueChangedEventArgs { x = x, y = y });
    }

    public T GetGridObjectValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            return gridArray[x + y * width].value;
        }
        else
        {
            return default(T);
        }
    }

    public T GetGridObjectValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObjectValue(x, y);
    }

    public GridObject<T> GridObject
    {
        get => default;
        set
        {
        }
    }
}
