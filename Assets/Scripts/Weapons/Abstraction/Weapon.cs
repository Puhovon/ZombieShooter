using System;
using System.Collections;
using Configs;
using UnityEngine;
using Zenject;

namespace Weapons.Abstraction
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private WeaponConfig _config;
        [SerializeField] private int _ammo;
        [SerializeField] private bool _canAttack = true;

        private InputSystem_Actions _input;
        private Coroutine _reloadCoroutine;
        private Coroutine _timerToReloadCoroutine;
        protected WeaponConfig Config => _config;
        protected int Ammo => _ammo;
        protected bool CanAttack
        {
            get => _canAttack;
            set => _canAttack = value;
        }

        protected InputSystem_Actions Input => _input;

        public Action<int> OnAmmoChanged; 

        [Inject]
        private void Construct(InputSystem_Actions input)
        {
            _input = input;
        }
        
        private void Awake()
        {
            _ammo = _config.MaxAmmo;
        }

        protected bool IsCanAttack()
        {
            if (!_canAttack)
            {
                return false;
            }
            _ammo -= 1;
            OnAmmoChanged?.Invoke(_ammo);
            _canAttack = false;
            return true;
        }


        public IEnumerator TimerReload()
        {
            print("Reload Timer");
            yield return new WaitForSeconds(_config.ReloadTime);
            _ammo = _config.MaxAmmo;
            _canAttack = true;
        }

        
        public IEnumerator TimerToNextShoot()
        {
            print("Timer To Next Shoot");
            yield return new WaitForSeconds(_config.TimeToNextShoot);
            _canAttack = true;
        }

        public virtual void Shoot() { }

        protected abstract void Raycasting();
    }
}