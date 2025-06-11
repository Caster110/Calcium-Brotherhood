using UnityEngine;
[CreateAssetMenu(fileName = "New Tile Resource", menuName = "Scriptable Objects/New Tile")]
public class TileResource : ScriptableObject, IResource
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Material Tileset { get; private set; }
    [field: SerializeField] public float FrameDuration { get; private set; }
    [field: SerializeField] public AnimationFrame[] AnimationFrames { get; private set; }
    [field: SerializeField] public TileData Data { get; private set; }

    public void InitFrames()
    {
        foreach (AnimationFrame frame in AnimationFrames)
        {
            frame.InitUVs(Tileset);
        }
    }
    [System.Serializable]
    public class AnimationFrame
    {
        [field: SerializeField] private Vector2Int UV00pixels;
        [field: SerializeField] private Vector2Int UV11pixels;

        [HideInInspector] public Vector2 UV00 { get; private set; }
        [HideInInspector] public Vector2 UV11 { get; private set; }

        public void InitUVs(Material tileset)
        {
            UV00 = new Vector2(UV00pixels.x / (float)tileset.mainTexture.width, 1 - UV00pixels.y / (float)tileset.mainTexture.height);
            UV11 = new Vector2(UV11pixels.x / (float)tileset.mainTexture.width, 1 - UV11pixels.y / (float)tileset.mainTexture.height);
        }
    }
    
}