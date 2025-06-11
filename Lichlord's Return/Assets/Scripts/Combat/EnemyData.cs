using System;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
[Serializable]
public class EnemyData : CombatEntityData
{
    public int MaxHealth;
    public int Initiative;
    public List<Ability> abilities;
    [SerializeReference] public CombatEntityBehaviour behaviour;
}
