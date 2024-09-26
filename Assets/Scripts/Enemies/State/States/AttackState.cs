using System.Collections;
using Configs.Enemy;
using Enemies.Abstractions;
using Enemies.State.States.Abstractions;
using Global.Abstractions;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Enemies.State.States
{
    public class AttackState : EnemyDefaultState
    {
        private Overlaper _overlaper;
        private bool _canAttack = true;
        private EnemyView _view;
        public AttackState(NavMeshAgent agent, EnemyConfig config, Enemy enemy, IStateSwitcher switcher, LayerMask mask,
            EnemyView enemyView) : base(agent, config, enemy, switcher)
        {
            _view = enemyView;
            _overlaper = new Overlaper(enemy.transform, Config.AttackConfig.attackRadius,mask, Config.AttackConfig.damage);
        }
        public override void Enter()
        {
            base.Enter();
            _view.StartAttack();
            Agent.isStopped = true;
        }

        public override void Exit()
        {
            base.Exit();
            _view.StopAttack();
            Agent.isStopped = false;
        }

        public override void Update()
        {
            base.Update();
            if (!IsPlayerInAttackDistance())
            {
                StateSwitcher.SwitchState<MovementState>();
                return;
            }
            if(!_canAttack)
                return;
            var colliders = _overlaper.Overlapping();
            Attack(colliders);
        }

        private void Attack(IDamagable colliders)
        {
            if(!_canAttack)
                return;
            _canAttack = false;
            colliders.TakeDamage(Config.AttackConfig.damage);
            Enemy.StartCoroutine(TimerToNextAttack());
        }

        private IEnumerator TimerToNextAttack()
        {
            yield return new WaitForSeconds(Config.AttackConfig.timeToNextAttack);
            _canAttack = true;
        }
    }
}