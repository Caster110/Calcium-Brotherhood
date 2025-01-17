using System;

[Serializable]
public class GridObject<T>
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public T value;
    public GridObject(int x, int y)
    {
        X = x;
        Y = y;
    }
}
