using System.Collections.Generic;
using UnityEngine;
public abstract class CombatEntity : IResourceHolder
{
    [field: SerializeField] public int ResourceID { get; private set; }
    [field: SerializeField] public int X { get; private set; }
    [field: SerializeField] public int Y { get; private set; }
    [field: SerializeField] public bool IsPassable { get; private set; }
    public int IsDestroyed { get; private set; }
    public abstract void Init(CombatHandler combatHandler, List<CombatEntity> targets);
}
