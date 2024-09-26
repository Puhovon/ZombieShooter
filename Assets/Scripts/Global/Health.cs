using System;
using Buffs;
using Configs;
using Global.Abstractions;
using UnityEngine;

namespace Global
{
    public class Health : MonoBehaviour, IDamagable, IHealable
    {
        [SerializeField] private HealthConfig _healthConfig;
        [SerializeField] private int _currentHealth;

        public event Action<int> OnHealthChanged;
        public event Action OnDie; 

        private void Start()
        {
            _currentHealth = _healthConfig.maxHealth;
            OnHealthChanged?.Invoke(_currentHealth);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 1)
                throw new ArgumentOutOfRangeException($"{nameof(damage)} canno't be null or less");
            print(damage);
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                OnDie?.Invoke();
            }
            OnHealthChanged?.Invoke(_currentHealth);
        }

        public void Heal(int heal)
        {
            if (_currentHealth + heal > _healthConfig.maxHealth)
            {
                heal = _currentHealth + heal - _healthConfig.maxHealth;
                _currentHealth = heal;
            }
            else
                _currentHealth += heal;
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }
}