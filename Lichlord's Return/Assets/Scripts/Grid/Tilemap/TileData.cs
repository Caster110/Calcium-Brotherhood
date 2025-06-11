using UnityEngine;
[System.Serializable]
public class TileData
{
    [field: SerializeField] public bool Passable { get; private set; }
    [field: SerializeField] public TileType Type { get; private set; }
    public enum TileType
    {
        Top, Right, Bottom, Left,
        TopLeft, TopRight, BottomRight, BottomLeft, Center
    }
}
