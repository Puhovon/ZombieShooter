using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage;
        [field: SerializeField] public int MaxAmmo;
        [field: SerializeField] public float TimeToNextShoot;
        [field: SerializeField] public int ReloadTime;
        [field: SerializeField] public float Distance;
        
    }
}