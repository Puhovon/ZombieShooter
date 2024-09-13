using System;
using Configs;
using Global.Abstractions;
using UnityEngine;

namespace Global
{
    public class Health : MonoBehaviour, IDamagable
    {
        [SerializeField] private HealthConfig _healthConfig;
        private int _currentHealth;

        public void TakeDamage(int damage)
        {
            if (damage < 1)
                throw new ArgumentOutOfRangeException($"{nameof(damage)} canno't be null or less");
            _currentHealth -= damage;
        }
    }
}