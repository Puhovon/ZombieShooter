using System;
using Configs;
using Global.Abstractions;
using UnityEngine;

namespace Global
{
    public class Health : MonoBehaviour, IDamagable
    {
        [SerializeField] private HealthConfig _healthConfig;
        [SerializeField] private int _currentHealth;

        private void Start()
        {
            _currentHealth = _healthConfig.maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 1)
                throw new ArgumentOutOfRangeException($"{nameof(damage)} canno't be null or less");
            print(damage);
            _currentHealth -= damage;
        }
    }
}