using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveAbility : Ability
{
    public MoveAbility(CombatEntity owner) : base(owner) { }

    public int targetX { get; private set; }
    public int targetY { get; private set; }

    public override AbilityCommand Use()
    {
        return null;
        //return new AbilityCommand(this, Owner);
    }
}
