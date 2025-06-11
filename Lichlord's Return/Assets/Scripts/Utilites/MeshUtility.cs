using UnityEngine;
public static class MeshUtility
{
    public static void CreateEmptyMesh(int cellCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)//std
    {
        vertices = new Vector3[4 * cellCount];
        uvs = new Vector2[4 * cellCount];
        triangles = new int[6 * cellCount];
    }
    public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, int x, int y, Vector2 cellSize, Vector2 uv00, Vector2 uv11) //std
    {
        Vector3 pos = new Vector3(x * cellSize.x, y * cellSize.y);
        int vIndex = index * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        vertices[vIndex0] = pos;
        vertices[vIndex1] = pos + new Vector3(0, cellSize.y);
        vertices[vIndex2] = pos + new Vector3(cellSize.x, cellSize.y);
        vertices[vIndex3] = pos + new Vector3(cellSize.x, 0);

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
    public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, int x, int y, Vector2 cellSize, Vector2 uv00, Vector2 uv11, bool isometric) //Iso
        {
            Vector3 pos = new Vector3((x + y) * cellSize.x, (x - y) * cellSize.y);

            int vIndex = index * 4;
            int vIndex0 = vIndex;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;

            vertices[vIndex] = pos;
            vertices[vIndex + 1] = pos + new Vector3(cellSize.x, cellSize.y);
            vertices[vIndex + 2] = pos + new Vector3(2 * cellSize.x, 0);
            vertices[vIndex + 3] = pos + new Vector3(cellSize.x, -cellSize.y);

            uvs[vIndex0] = new Vector2(uv00.x, uv11.y);
            uvs[vIndex1] = new Vector2(uv11.x, uv11.y);
            uvs[vIndex2] = new Vector2(uv11.x, uv00.y);
            uvs[vIndex3] = new Vector2(uv00.x, uv00.y);

            int tIndex = index * 6;

            triangles[tIndex + 0] = vIndex0;
            triangles[tIndex + 1] = vIndex1;
            triangles[tIndex + 2] = vIndex2;

            triangles[tIndex + 3] = vIndex0;
            triangles[tIndex + 4] = vIndex2;
            triangles[tIndex + 5] = vIndex3;
    }
    public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, int x, int y, Vector2 cellSize, Vector2 uv00, Vector2 uv11, int isometric) //Iso experimental
    {
        float halfX = cellSize.x * 0.5f;
        float halfY = cellSize.y * 0.5f;
        Vector3 pos = new Vector3((x - y) * halfX, -(x + y) * halfY);

        int vIndex = index * 4;
        vertices[vIndex] = pos + new Vector3(-halfX, 0);
        vertices[vIndex + 1] = pos + new Vector3(0, halfY);
        vertices[vIndex + 2] = pos + new Vector3(halfX, 0);
        vertices[vIndex + 3] = pos + new Vector3(0, -halfY);

        uvs[vIndex] = new Vector2(uv00.x, uv11.y);
        uvs[vIndex + 1] = new Vector2(uv11.x, uv11.y);
        uvs[vIndex + 2] = new Vector2(uv11.x, uv00.y);
        uvs[vIndex + 3] = new Vector2(uv00.x, uv00.y);

        int tIndex = index * 6;
        triangles[tIndex] = vIndex;
        triangles[tIndex + 1] = vIndex + 1;
        triangles[tIndex + 2] = vIndex + 2;
        triangles[tIndex + 3] = vIndex;
        triangles[tIndex + 4] = vIndex + 2;
        triangles[tIndex + 5] = vIndex + 3;
    }
}
