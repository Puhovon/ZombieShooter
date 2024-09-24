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
        [SerializeField] private Transform _finder;
        [SerializeField] private Transform _player;

        [SerializeField] private float _disstance;
        
        private EnemyStateMachine _stateMachine;

        public Transform Finder => _finder;
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
            _disstance = (_finder.position - Player.position).magnitude;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(Finder.position, _config.AttackConfig.attackRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Finder.position, _config.MoveConfig.distanceToAttack);
        }
    }
}