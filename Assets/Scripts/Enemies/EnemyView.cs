using System;
using Global;
using UnityEngine;

namespace Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Health _health;

        private const string Running = "IsRunning";
        private const string Attack = "IsAttack";
        private const string Dying = "IsDying";

        public event Action onAttackAnimationEnd;

        private void Start()
        {
            _health.OnDie += Die;
        }

        public void StartRunning()
        {
            _animator.SetBool(Running, true);    
        }

        public void StartAttack()
        {
            _animator.SetBool(Attack, true);
        }
        
        public void StopRunning()
        {
            _animator.SetBool(Running, false);
        }

        public void StopAttack()
        {
            _animator.SetBool(Attack, false);
        }

        private void Die()
        {
            StopAttack();
            StopRunning();
            _animator.SetBool(Dying, true);
        }
        
        public void OnAttackAnimationEnd() {
            onAttackAnimationEnd?.Invoke();
        }
    }
}