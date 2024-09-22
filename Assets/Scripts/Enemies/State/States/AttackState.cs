using Configs.Enemy;
using Enemies.Abstractions;
using Enemies.State.States.Abstractions;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Enemies.State.States
{
    public class AttackState : EnemyDefaultState
    {
        private Overlaper _overlaper;
        public AttackState(NavMeshAgent agent, EnemyConfig config, Enemy enemy, IStateSwitcher switcher, LayerMask mask) : base(agent, config, enemy, switcher)
        {
            _overlaper = new Overlaper(enemy.transform, Config.AttackConfig.attackRadius,mask, Config.AttackConfig.damage);
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
            _overlaper.Overlapping();
        }
    }
}