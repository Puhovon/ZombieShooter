using Buffs;
using Global;
using Unity.Mathematics;
using UnityEngine;

namespace Enemies
{
    public class EnemyDie : MonoBehaviour
    {
        [SerializeField] private FirstAidKit _aidKit;
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.OnDie += OnDie;
        }

        private void OnDie()
        {
            Instantiate(_aidKit, transform.position, quaternion.identity, null);
        }

        private void OnDisable()
        {
            _health.OnDie -= OnDie;
        }
    }
}