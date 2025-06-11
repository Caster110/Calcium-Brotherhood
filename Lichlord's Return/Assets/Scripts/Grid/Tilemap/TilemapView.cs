using System.Collections.Generic;
using UnityEngine;
public class TilemapView
{
    public float minimalTickDuration { get; private set; }
    private int currentFrame;
    private Grid<Tile> renderableGrid;
    private Mesh mesh;
    private bool update;
    private Vector2 cellSize;
    private readonly Dictionary<int, TileResource> resourceDictionary;
    public TilemapView(Mesh mesh, TilemapModel model, Vector2 cellSize)
    {
        currentFrame = 0;
        renderableGrid = model.Grid;
        resourceDictionary = ResourceDB.Tiles;
        this.cellSize = cellSize;
        this.mesh = mesh;
        update = true;
        model.OnGridChanged += UpdateTilemapFlag;
    }

    public void ChangeFrame()
    {
        currentFrame++;
        AnimationTick();
    }
    public void ForLateUpdate()
    {
        if (update)
        {
            AnimationTick();
            update = false;
        }
    }
    public void UpdateTilemapFlag(object sender, TilemapModel.OnMChangedEventArgs e)
    {
        renderableGrid = e.Grid;
        update = true;
    }
    private void AnimationTick()
    {
        MeshUtility.CreateEmptyMesh(renderableGrid.Width * renderableGrid.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < renderableGrid.Width; x++)
        {
            for (int y = 0; y < renderableGrid.Height; y++)
            {
                int index = x * renderableGrid.Height + y;

                Tile tile = renderableGrid.GetGridObjectValue(x, y);
                if (tile != null)
                {
                    TileResource resource = resourceDictionary[tile.ResourceID];
                    Vector2 UV00 = resource.AnimationFrames[currentFrame % resource.AnimationFrames.Length].UV00;
                    Vector2 UV11 = resource.AnimationFrames[currentFrame % resource.AnimationFrames.Length].UV11;

                    MeshUtility.AddToMeshArrays(vertices, uv, triangles, index, x, y,
                        cellSize, UV00, UV11);
                }
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}