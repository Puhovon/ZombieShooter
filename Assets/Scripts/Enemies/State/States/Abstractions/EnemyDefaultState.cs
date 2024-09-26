using Configs.Enemy;
using Enemies.Abstractions;
using UnityEngine.AI;

namespace Enemies.State.States.Abstractions
{
    public abstract class EnemyDefaultState : IEnemyState
    {
        private NavMeshAgent _agent;
        private EnemyConfig _config;
        private Enemy _enemy;
        
        private IStateSwitcher _switcher;
        protected NavMeshAgent Agent => _agent;
        protected EnemyConfig Config => _config;
        protected Enemy Enemy => _enemy;
        protected IStateSwitcher StateSwitcher => _switcher;

        public EnemyDefaultState(NavMeshAgent agent, EnemyConfig config, Enemy enemy, IStateSwitcher switcher)
        {
            _agent = agent;
            _config = config;
            _enemy = enemy;
            _switcher = switcher;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Update()
        {
            
        }

        protected bool IsPlayerInAttackDistance()
        {
            return (Enemy.transform.position - Enemy.Player.position).magnitude < Config.MoveConfig.distanceToAttack;
        }
    }
}