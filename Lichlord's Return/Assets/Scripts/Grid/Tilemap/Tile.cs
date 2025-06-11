using UnityEngine;
[System.Serializable]
public class Tile : IResourceHolder
{
    [field:SerializeField] public int ResourceID { get; private set; }
    public Tile(int resourceID = 0)
    {
        ResourceID = resourceID;
    }
}
