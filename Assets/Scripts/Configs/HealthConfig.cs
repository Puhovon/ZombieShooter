using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Health", menuName = "Configs/Health")]
    public class HealthConfig : ScriptableObject
    {
        [field: SerializeField] public int maxHealth;
    }
}