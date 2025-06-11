using UnityEngine;
using System.Collections.Generic;
using System;
public class Enemy: CombatEntity, ITurnHandler
{
    public int MovementPoints { get; private set; }

    private int health;
    private int maxHealth;
    public int Initiative { get; private set; }
    public int NumberInQueue { get; set; } = -1;

    private MoveAbility moveAbility;
    private List<Ability> abilities;
    [NonSerialized] private bool[,] passabilityMap;
    [NonSerialized] private CombatHandler combatHandler;
    [NonSerialized] private List<CombatEntity> targets;


    public Enemy(int ResourceID) 
    {
        //this.behaviour = behaviour;
    }
    public override void Init(CombatHandler combatHandler, List<CombatEntity> entities)
    {
        EnemyData data = ResourceDB.Enemies[ResourceID].Data;
        passabilityMap = combatHandler.GetPassabilityMap();
        maxHealth = data.MaxHealth;
        Initiative = data.Initiative;
        abilities = data.abilities;
        this.targets = entities;
        this.combatHandler = combatHandler;
        moveAbility = new MoveAbility(this);
        //effects apply
    }
    public void HandleTurn()
    {
        //behaviur smth
        //find path
        combatHandler.AddToAbilityUsageQueue(moveAbility.Use());
    }
}
