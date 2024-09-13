using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float VerticalRotationLowerBound;
        [field: SerializeField] public float VerticalRotationHigherBound;
        [field: SerializeField] public float RotateSpeedX;
        [field: SerializeField] public float RotateSpeedY;
    }
}