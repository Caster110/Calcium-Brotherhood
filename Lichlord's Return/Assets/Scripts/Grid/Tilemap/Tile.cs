using UnityEngine;
[CreateAssetMenu(fileName = "New Tile", menuName = "Scriptable Objects/Tile")]
public class Tile : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public int tilesetNumber { get; private set; }
    [field: SerializeField] public Type type { get; private set; }
    [field: SerializeField] public Vector2Int uv00Pixels { get; private set; }
    [field: SerializeField] public Vector2Int uv11Pixels { get; private set; }
    [HideInInspector] public Vector2 uv00;
    [HideInInspector] public Vector2 uv11;
}
public enum Type {
    Top,
    Right,
    Bottom,
    Left,
    TopLeft,
    TopRight,
    BottomRight,
    BottomLeft,
    Center,
}