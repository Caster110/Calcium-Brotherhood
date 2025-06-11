using UnityEngine;
[CreateAssetMenu(fileName = "New Ability Resource", menuName = "Scriptable Objects/New Ability")]
public class AbilityResource : ScriptableObject, IResource
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    //some visual thing
    [field: SerializeField] public AbilityData Data;

}
