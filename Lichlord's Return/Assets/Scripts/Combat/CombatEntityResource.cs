using System;
using UnityEngine;
[SerializeField]
[Serializable]
public abstract class CombatEntityResource : ScriptableObject, IResource 
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public GameObject GameObject { get; private set; }
}
