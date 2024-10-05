using System;
using Global;
using UnityEngine;

namespace Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Health _health;
        [SerializeField] private AkEvent _runningEvent;
        [SerializeField] private AkEvent _attackEvent;
        [SerializeField] private string _runningEventName;
        [SerializeField] private string _attackEventName;

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
            AkSoundEngine.PostEvent(_runningEventName, gameObject);
            // _runningEvent.HandleEvent(gameObject);
        }

        public void StartAttack()
        {
            _animator.SetBool(Attack, true);
            AkSoundEngine.PostEvent(_attackEventName, gameObject);
            // _attackEvent.HandleEvent(gameObject);
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