using UnityEngine;

namespace Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private const string Running = "IsRunning";
        private const string Attack = "IsAttack";
        
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
    }
}