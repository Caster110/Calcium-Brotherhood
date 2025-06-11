using System.Collections.Generic;
using UnityEngine;
public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private bool devMode = false;
    [Header("Tilemap Grid")]
    [SerializeField] private MeshRenderer tilemapMeshRenderer;
    [SerializeField] private MeshFilter tilemapMeshFilter;
    [Header("Input Grid")]
    [SerializeField] private MeshRenderer inputMeshRenderer;
    [SerializeField] private MeshFilter inputMeshFilter;
    [Header("Grid Configuration")]
    [SerializeField] private bool newGrid;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Vector2 cellSize;
    [Header("Tilemap Editing")]
    [SerializeField] private TilemapEditor tilemapEditor;
    [SerializeField] private GridInputHandler gridInput;
    [SerializeField] private TilemapAnimator tilemapAnimator;
    [SerializeField] private new Camera camera;
    [SerializeField] private CombatGameplayUI UI;

    [SerializeField] private CombatHandlerView combatHandlerView;

    private bool tilesInited;
    private bool enemiesInited;
    private bool trapsInited;
    private bool abilitiesInited;

    private void Awake()
    {
        if (devMode)
            DevInit();
        else
            PlayerInit();
    }

    private void DevInit()
    {
        Dictionary<int, TileResource> TileResourceDictionary = new();
        Grid<Tile> grid;
        if (!newGrid)
        {
            grid = SaveSystem.LoadMostRecentObject<Grid<Tile>>();

            if (grid == null)
            {
                grid = new Grid<Tile>(width, height, new Tile(1));
            }
        }
        else
        {
            grid = new Grid<Tile>(width, height, new Tile(0));
        }

        ResourceLoader.Load("Tiles", TileResourceDictionary, () => 
        tilemapEditor.Init(InitTilemap(grid, tilemapMeshRenderer, tilemapMeshFilter, cellSize), gridInput), resource =>
        {
            if (resource is TileResource tile)
            {
                tile.InitFrames();
                return tile;
            }
            Debug.LogError("Labels error");
            return default;
        });

        gridInput.Init(camera, cellSize);
    }
    private void PlayerInit()
    {
        Dictionary<int, TileResource> tileResourceDictionary = new();
        Dictionary<int, EnemyResource> enemyResourceDictionary = new();
        Dictionary<int, TrapResource> trapResourceDictionary = new();
        Dictionary<int, AbilityResource> abilityResourceDictionary = new();
        ResourceDB.UpdateDB(tileResourceDictionary, enemyResourceDictionary, trapResourceDictionary, abilityResourceDictionary);

        ResourceLoader.Load("Tiles", tileResourceDictionary, () =>
        {
            tilesInited = true;
            OnResourcesLoaded();
        }, resource =>
        {
            if (resource is TileResource tile)
            {
                tile.InitFrames();
                return tile;
            }
            Debug.LogError("Label/Type mismatch error");
            return default;
        });

        ResourceLoader.Load("Enemies", enemyResourceDictionary, () =>
        {
            enemiesInited = true;
        }, resource =>
        {
            if (resource is EnemyResource combatEntity) return combatEntity;

            Debug.LogError("Label/Type mismatch error");
            return default;
        });

        ResourceLoader.Load("Traps", trapResourceDictionary, () =>
        {
            trapsInited = true;
            OnResourcesLoaded();
        }, resource =>
        {
            if (resource is TrapResource combatEntity) return combatEntity;

            Debug.LogError("Label/Type mismatch error");
            return default;
        });

        ResourceLoader.Load("Abilities", abilityResourceDictionary, () =>
        {
            abilitiesInited = true;
            OnResourcesLoaded();
        }, resource =>
        {
            if (resource is AbilityResource ability) return ability;

            Debug.LogError("Label/Type mismatch error");
            return default;
        });
        
        
        gridInput.Init(camera, cellSize);
    }
    private TilemapModel InitTilemap(Grid<Tile> grid, MeshRenderer meshRenderer, MeshFilter meshFilter, Vector2 cellSize)
    {
        TilemapModel model = new();
        model.InitNewGrid(grid);

        Mesh mesh = new();
        meshRenderer.material = ResourceDB.Tiles[0].Tileset;
        meshFilter.mesh = mesh;

        TilemapView view = new(mesh, model, cellSize);

        tilemapAnimator.AddTilemap(view);

        return model;
    }
    private CombatHandler InitCombatHandler(CombatHandlerData combatHandlerData, Grid<Tile> map)
    {
        List<CombatEntity> entities = new();
        entities.Add(new Character());
        combatHandlerData = new(0, entities, map);
        CombatHandler combatHandler = new(combatHandlerData);
        combatHandlerView.Init(combatHandler, cellSize);
        return combatHandler;
    }
    private void OnResourcesLoaded()
    {

        if (tilesInited && trapsInited && enemiesInited && abilitiesInited)
        {
            Grid<Tile> tilemapGrid;
            if (newGrid)
            {
                tilemapGrid = new Grid<Tile>(width, height);
            }
            else
            {
                tilemapGrid = SaveSystem.LoadMostRecentObject<Grid<Tile>>();

                if (tilemapGrid == null)
                {
                    tilemapGrid = new Grid<Tile>(width, height);
                }
            }

            CombatHandlerData combatHandlerData = SaveSystem.LoadMostRecentObject<CombatHandlerData>();
            TilemapModel inputMap = InitTilemap(tilemapGrid, tilemapMeshRenderer, tilemapMeshFilter, cellSize);
            CombatHandler combatHandler = InitCombatHandler(combatHandlerData, tilemapGrid);

            CombatInputHandler combatInputHandler = new();
            combatInputHandler.Init(inputMap, gridInput, combatHandler);
            combatHandler.StartCombat();
        }
    }
}
