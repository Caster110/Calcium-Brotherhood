using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbilityCommand : Command
{
    public Ability origin;
    public CombatEntity Invoker;
    public AbilityCommand(Ability origin, CombatEntity invoker)
    {
        this.origin = origin;
        Invoker = invoker;
    }
}
