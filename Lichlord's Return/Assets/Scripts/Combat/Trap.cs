using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trap : CombatEntity
{
    private List<Ability> abilities;
    [NonSerialized] private CombatHandler combatHandler;
    public Trap(int ResourceID)
    {
        //this.behaviour = behaviour;
    }
    public override void Init(CombatHandler combatHandler, List<CombatEntity> entities)
    {
        TrapData data = new TrapData();
        abilities = data.abilities;
        //this.targets = entities;
        this.combatHandler = combatHandler;
        //effects apply
    }
}
