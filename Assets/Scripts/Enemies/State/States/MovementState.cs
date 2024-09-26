using Configs.Enemy;
using Enemies.Abstractions;
using Enemies.State.States.Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.State.States
{
    public class MovementState : EnemyDefaultState
    {
        private EnemyView _view;
        public MovementState(NavMeshAgent agent, EnemyConfig config, Enemy enemy, IStateSwitcher stateSwitcher,
            EnemyView enemyView) : base(agent, config, enemy, stateSwitcher)
        {
            _view = enemyView;
        }

        public override void Enter()
        {
            base.Enter();
            _view.StartRunning();
        }

        public override void Exit()
        {
            base.Exit();
            _view.StopRunning();
        }

        public override void Update()
        {
            base.Update();
            if (IsPlayerInAttackDistance())
            {
                StateSwitcher.SwitchState<AttackState>();
                return;
            }
            Agent.SetDestination(Enemy.Player.position);
        }
    }
}