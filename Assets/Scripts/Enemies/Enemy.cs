using System;
using Configs.Enemy;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private LayerMask _mask;

        private EnemyStateMachine _stateMachine;
        [SerializeField] private Transform _player;

        public Transform Player => _player;

        [Inject]
        private void Construct(Transform player)
        {
            _player = player;
        }
        
        private void Awake()
        {
            _stateMachine = new EnemyStateMachine(_agent, _config, this, _mask);
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, _config.AttackConfig.attackRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _config.MoveConfig.distanceToAttack);
        }
    }
}