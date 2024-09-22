using Configs.Enemy;
using Enemies.Abstractions;
using Enemies.State.States.Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.State.States
{
    public class MovementState : EnemyDefaultState
    {
        public MovementState(NavMeshAgent agent, EnemyConfig config, Enemy enemy, IStateSwitcher stateSwitcher) : base(agent, config, enemy, stateSwitcher)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log(GetType());
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if ((Enemy.transform.position - Enemy.Player.position).magnitude < Config.MoveConfig.distanceToAttack)
            {
                _stateSwitcher.SwitchState<AttackState>();
            }
            Agent.SetDestination(Enemy.Player.position);
        }
    }
}