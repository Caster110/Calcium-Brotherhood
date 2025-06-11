using System.Collections.Generic;

public static class ResourceDB
{
    public static Dictionary<int, TileResource> Tiles;
    public static Dictionary<int, EnemyResource> Enemies;
    public static Dictionary<int, TrapResource> Traps;
    public static Dictionary<int, AbilityResource> Abilities;

    public static void UpdateDB(Dictionary<int, TileResource> tiles,
        Dictionary<int, EnemyResource> enemies, Dictionary<int, TrapResource> traps,
        Dictionary<int, AbilityResource> abilities)
    {
        Tiles = tiles;
        Enemies = enemies;
        Abilities = abilities;
        Traps = traps;
    }
}
