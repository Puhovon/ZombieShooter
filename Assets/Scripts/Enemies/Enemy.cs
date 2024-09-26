using System.Collections;
using Configs.Enemy;
using Global;
using TMPro;
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
        [SerializeField] private Health _health;
        [SerializeField] private EnemyView _view;

        [SerializeField] private float _distance;
        
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
            _stateMachine = new EnemyStateMachine(_agent, _config, this, _mask, _view);
            _health.OnDie += OnDie;
        }

        private void Update()
        {
            _stateMachine.Update();
            _distance = (_finder.position - Player.position).magnitude;
        }
        
        private void OnDie()
        {
            _agent.isStopped = true;
            StartCoroutine(DieTimer());
        }

        private IEnumerator DieTimer()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
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