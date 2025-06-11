using System;
using System.Collections.Generic;
public class CombatHandler
{
    public class AbilityEventArgs : EventArgs
    {
        public AbilityCommand abilityUsage;
    }
    public event EventHandler<AbilityEventArgs> abilityUsed;
    public event EventHandler<ITurnHandler> turnHandlerChanged;

    public readonly List<CombatEntity> combatEntities;
    private bool[,] passabilityMap;
    private int currentTurnHandler;
    private Grid<Tile> map;
    private Stack<AbilityCommand> abilityUsageQueue;
    private List<ITurnHandler> turnParticipants;
    public CombatHandler(CombatHandlerData data)
    {
        abilityUsageQueue = new();
        turnParticipants = new();
        currentTurnHandler = data.currentTurnHandler;
        map = data.map;
        combatEntities = data.combatEntities;
        for (int i = 0; i < combatEntities.Count; i++)
        {
            if(combatEntities[i] is ITurnHandler handler)
            {
                turnParticipants.Add(handler);
            }
            //combatEntities?.Init();
            combatEntities[i].Init(this, combatEntities);//hueta
        }
        SortTurnHandlers(turnParticipants);
    }
    public void StartCombat()
    {
        turnParticipants[currentTurnHandler].HandleTurn();
        turnHandlerChanged?.Invoke(this, turnParticipants[currentTurnHandler]);
    }
    public void SortTurnHandlers(List<ITurnHandler> queue)
    {
        List<ITurnHandler> assignedQueue = new();
        List<ITurnHandler> unassignedQueue = new();

        foreach (ITurnHandler enemy in queue)
        {
            if (enemy.NumberInQueue >= 0)
                assignedQueue.Add(enemy);
            else if (enemy.NumberInQueue == -1)
                unassignedQueue.Add(enemy);
        }

        assignedQueue.Sort((a, b) => a.NumberInQueue.CompareTo(b.NumberInQueue));

        queue.Clear();
        queue.AddRange(assignedQueue);

        foreach (ITurnHandler unassignedEntity in unassignedQueue)
        {
            int lastIndex = -1;
            for (int i = 0; i < queue.Count; i++)
            {
                if (queue[i].Initiative == unassignedEntity.Initiative)
                {
                    lastIndex = i;
                }
                else if (queue[i].Initiative < unassignedEntity.Initiative)
                {
                    break;
                }
            }

            if (lastIndex == -1)
            {
                queue.Add(unassignedEntity);
            }
            else
            {
                queue.Insert(lastIndex + 1, unassignedEntity);
            }
        }

        int bias = 0;

        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i].NumberInQueue == -1 && i < currentTurnHandler)
                bias++;
            queue[i].NumberInQueue = i;
        }
        currentTurnHandler += bias;
    }
    public void AddToAbilityUsageQueue(AbilityCommand ability)
    {
        abilityUsageQueue.Push(ability);
        ExecuteAbilityCommand();
    }
    public void AddToAbilityUsageQueue(List<AbilityCommand> abilities)
    {
        foreach (AbilityCommand ability in abilities)
            AddToAbilityUsageQueue(abilities);
    }
    private void ExecuteAbilityCommand()
    {
        AbilityCommand abilityUsage = abilityUsageQueue.Pop();
        abilityUsage.Execute();
        abilityUsed?.Invoke(this, new AbilityEventArgs { abilityUsage = abilityUsage });
    }
    public CombatHandlerData GetSaveData()
    {
        return new CombatHandlerData(currentTurnHandler, combatEntities, map);
    }
    public bool[,] GetPassabilityMap() 
    {
        if (passabilityMap != null)
            return passabilityMap;

        passabilityMap = new bool[map.Width, map.Height];
        for (int x = 0; x < map.Height; x++)
        {
            for (int y = 0; y < map.Width; y++)
            {
                int id = map.GetGridObjectValue(x, y).ResourceID;
                passabilityMap[x, y] = ResourceDB.Tiles[id].Data.Passable;
            }
        }
        foreach(CombatEntity entity in combatEntities)
        {
            passabilityMap[entity.X, entity.Y] = entity.IsPassable;
        }
        return passabilityMap;
    }
}
