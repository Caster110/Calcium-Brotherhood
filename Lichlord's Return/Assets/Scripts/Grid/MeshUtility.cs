using UnityEngine;
public static class MeshUtility
{
    public static void CreateEmptyMesh(int cellCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * cellCount];
        uvs = new Vector2[4 * cellCount];
        triangles = new int[6 * cellCount];
    }
    public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, Vector3 pos, float rot, Vector3 cellSize, Vector2 uv00, Vector2 uv11)
    {
        int vIndex = index * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        vertices[vIndex0] = pos + new Vector3(-cellSize.x / 2, -cellSize.y / 2);
        vertices[vIndex1] = pos + new Vector3(-cellSize.x / 2, cellSize.y / 2);
        vertices[vIndex2] = pos + new Vector3(cellSize.x / 2, cellSize.y / 2);
        vertices[vIndex3] = pos + new Vector3(cellSize.x / 2, -cellSize.y / 2);

        uvs[vIndex0] = new Vector2(uv00.x, uv00.y);
        uvs[vIndex1] = new Vector2(uv00.x, uv11.y);
        uvs[vIndex2] = new Vector2(uv11.x, uv11.y);
        uvs[vIndex3] = new Vector2(uv11.x, uv00.y);

        int tIndex = index * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;
    }
}
