using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GrenadeConfig", menuName = "Configs/Grenade")]
    public class GrenadeConfig : ScriptableObject
    {
        [field: SerializeField, Range(20, 150)] public float damage;
        [field: SerializeField] public float radius;
        [field: SerializeField, Min(1.5f)] public float timeBeforeExplore;
        [field: SerializeField, Min(1)] public int maxGrenade;
    }
}