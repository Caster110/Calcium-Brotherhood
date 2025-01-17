using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;
[RequireComponent (typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TilemapView : MonoBehaviour
{
    private Dictionary<int, Tile> tileDictionary = new Dictionary<int, Tile>();
    private Grid<int> grid;
    private Mesh mesh;
    private bool updateTilemap = false;

    public Tile Tile
    {
        get => default;
        set
        {
        }
    }

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Texture texture = GetComponent<MeshRenderer>().material.mainTexture;
        float textureWidth = texture.width;
        float textureHeight = texture.height;

        Addressables.LoadAssetsAsync<Tile>("Tiles", tile =>
        {
            if (!tileDictionary.ContainsKey(tile.ID))
                tileDictionary.Add(tile.ID, tile);
            else
                Debug.LogWarning($"Duplicate ID detected: {tile.ID} for tile {tile.name}");

            tile.uv00 = new Vector2(tile.uv00Pixels.x / textureWidth, tile.uv00Pixels.y / textureHeight);
            tile.uv11 = new Vector2(tile.uv11Pixels.x / textureWidth, tile.uv11Pixels.y / textureHeight);
            Debug.Log($"Loaded: {tile.name}, {tile.ID}");
        }).Completed += OnLoadCompleted;
    }
    private void OnLoadCompleted(AsyncOperationHandle<IList<Tile>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Successfully loaded {tileDictionary.Count} tiles.");
        }
        updateTilemap = true;
    }
    public void SetGrid(Grid<int> grid)
    {
        this.grid = grid;

        grid.OnGridObjectValueChanged += Grid_OnGridValueChanged;
        updateTilemap = true;
    }

    private void Grid_OnGridValueChanged(object sender, OnGridObjectValueChangedEventArgs e)
    {
        updateTilemap = true;
    }
    private void LateUpdate()
    {
        if (updateTilemap)
        {
            updateTilemap = false;
            UpdateTilemapVisual();
        }
    }
    private void UpdateTilemapVisual()
    {
        MeshUtility.CreateEmptyMesh(grid.Width * grid.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                int index = x * grid.Height + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.CellSize;

                int tileID = grid.GetGridObjectValue(x, y);
                if (tileID >= tileDictionary.Count)
                {
                    tileID = tileDictionary.Count - 1;
                }
                Vector2 gridUV00, gridUV11;
                gridUV00 = tileDictionary[tileID].uv00;
                gridUV11 = tileDictionary[tileID].uv11;
                MeshUtility.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridUV00, gridUV11);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
