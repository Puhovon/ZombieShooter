using UnityEngine;

namespace Configs.Enemy
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Configs/WaveConfig")]
    public class WaveConfig : ScriptableObject
    {
        [field: SerializeField] public int StartEnemiesCount;
        [field: SerializeField] public int CountMagnifier;
    }
}