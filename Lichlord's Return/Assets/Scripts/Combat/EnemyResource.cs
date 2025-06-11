using System;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Resource", menuName = "Scriptable Objects/New Combat Entity/New Enemy")]
[SerializeField]
[Serializable]
public class EnemyResource : CombatEntityResource
{
    [field: SerializeField] public EnemyData Data { get; private set; }
}
