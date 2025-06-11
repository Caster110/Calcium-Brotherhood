using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : CombatEntity, ITurnHandler
{
    public new int ResourceID { get; set; }// cal
    public int MovementPoints { get ; set ; }
    public int Initiative => throw new System.NotImplementedException();
    public int NumberInQueue { get ; set ; }
    public override void Init(CombatHandler combatHandler, List<CombatEntity> targets)
    {
        ResourceID = 0;
        //moveAbility = new MoveAbility(this);
        Debug.LogWarning("Init govna");
    }
    public void HandleTurn()
    {

        Debug.LogWarning("handle govna");
    }


}
