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
        [field: SerializeField] public float attackRadius;
        [field: SerializeField] public int damage;
    }
    [Serializable]
    public class MoveConfig
    {
        [field: SerializeField] public float speed;
        [field: SerializeField] public float distanceToAttack;
    }
}