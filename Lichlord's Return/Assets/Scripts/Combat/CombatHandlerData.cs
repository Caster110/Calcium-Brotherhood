using System.Collections.Generic;
using System;
[Serializable]
public class CombatHandlerData 
{
    public readonly int currentTurnHandler;
    public readonly List<CombatEntity> combatEntities;
    public readonly Grid<Tile> map;
    public CombatHandlerData(int currentTurnHandler, List<CombatEntity> combatEntities, Grid<Tile> map)
    {
        this.currentTurnHandler = currentTurnHandler;
        this.combatEntities = combatEntities;
        this.map = map;
    }
}
