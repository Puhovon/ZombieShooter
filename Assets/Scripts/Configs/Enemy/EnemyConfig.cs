using System;
using UnityEngine;

namespace Configs.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private AttackConfig _attackConfig;
        [SerializeField] private MoveConfig _moveConfig;

        public AttackConfig AttackConfig => _attackConfig;
        public MoveConfig MoveConfig => _moveConfig;
    }

    [Serializable]
    public class AttackConfig
    {
        [field: SerializeField, Min(1)] public float attackRadius;
        [field: SerializeField, Min(5)] public int damage;
        [field: SerializeField, Min(0.1f)] public float timeToNextAttack;
    }
    [Serializable]
    public class MoveConfig
    {
        [field: SerializeField, Min(1)] public float speed;
        [field: SerializeField, Min(1)] public float distanceToAttack;
    }
}