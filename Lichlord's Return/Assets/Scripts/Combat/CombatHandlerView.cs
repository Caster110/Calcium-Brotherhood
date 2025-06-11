using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class CombatHandlerView : MonoBehaviour 
{
    private bool update;
    private Vector2 cellSize;
    private Dictionary<int, EnemyResource> enemyResourceDictionary;
    private Dictionary<int, TrapResource> trapResourceDictionary;
    private Dictionary<int, AbilityResource> abilityResourceDictionary;
    private Dictionary<CombatEntity, CombatEntityObjectVisual> entitiesVisual;
    public void Init(CombatHandler model, Vector2 cellSize)
    {
        this.cellSize = cellSize;
        enemyResourceDictionary = ResourceDB.Enemies;
        trapResourceDictionary = ResourceDB.Traps;
        abilityResourceDictionary = ResourceDB.Abilities;
        entitiesVisual = new();
        foreach (CombatEntity entity in model.combatEntities)
        {
            SpawnEntity(entity);
        }
        update = true;
        model.abilityUsed += AnimateChanges;
        UpdateCombatVisual();
    }
    private void SpawnEntity(CombatEntity entity)
    {
        switch (entity)
        {
            case Enemy enemy:
                entitiesVisual.Add(enemy, new(Instantiate(enemyResourceDictionary[enemy.ResourceID].GameObject,
                    new Vector3(cellSize.x * (enemy.X + 0.5f), cellSize.y * (enemy.Y + 0.5f), 0), Quaternion.identity)));
                break;

            case Trap trap:
                entitiesVisual.Add(trap, new(Instantiate(trapResourceDictionary[trap.ResourceID].GameObject,
                    new Vector3(cellSize.x * (trap.X + 0.5f), cellSize.y * (trap.Y + 0.5f), 0), Quaternion.identity)));
                break;

            case Character chara:
                entitiesVisual.Add(chara, new(Instantiate(enemyResourceDictionary[0].GameObject,
                    new Vector3(cellSize.x * (chara.X + 0.5f), cellSize.y * (chara.Y + 0.5f), 0), Quaternion.identity)));
                break;

            default:
                Debug.LogWarning($"Unknown entity type: {entity.GetType().Name}");
                break;
        }
    }
    private void AnimateChanges(object sender, CombatHandler.AbilityEventArgs e)
    {
        AbilityCommand ability = e.abilityUsage;
        switch (ability.origin)
        {
            case MoveAbility ma:
                entitiesVisual[ma.Owner].sequence.Append(entitiesVisual[ma.Owner].transform.DOMove(new Vector3(ma.targetX * cellSize.x, ma.targetY * cellSize.y), 2));
                break;
        }
    }

    public void ForLateUpdate()
    {
        if (update)
        {
            UpdateCombatVisual();
            update = false;
        }
    }

    private void UpdateCombatVisual()
    {
        
    }
    public class CombatEntityObjectVisual
    {
        public readonly Animator animator;
        public readonly Transform transform;
        public readonly Sequence sequence;
        public CombatEntityObjectVisual(GameObject gameObject)
        {
            animator = gameObject.GetComponent<Animator>();
            transform = gameObject.transform;
            sequence = DOTween.Sequence();// не всем объектам нужно (пока)
        }
    }
}
