using UnityEngine;
[CreateAssetMenu(fileName = "New Combat Entity Resource", menuName = "Scriptable Objects/New Combat Entity/New Trap")]
public class TrapResource : CombatEntityResource
{
    [field: SerializeField] public TrapData Data;
}
