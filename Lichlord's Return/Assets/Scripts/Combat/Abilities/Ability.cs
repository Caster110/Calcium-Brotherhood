using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Ability : IResourceHolder
{
    [field: SerializeField] public int ResourceID { get; private set; }
    public AbilityData Data;
    public CombatEntity Owner;
    public Ability(CombatEntity owner)
    {
        Owner = owner;
    }
    public abstract AbilityCommand Use();
}
