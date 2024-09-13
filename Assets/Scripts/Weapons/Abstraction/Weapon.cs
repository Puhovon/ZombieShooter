using System.Collections;
using Configs;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Weapons.Abstraction
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _config;
        [SerializeField] private int _ammo;
        private InputSystem_Actions _input;
        private Coroutine _coroutine;
        [SerializeField] private bool _canAttack = true;
        protected WeaponConfig Config => _config;
        protected int Ammo => _ammo;
        protected bool CanAttack
        {
            get => _canAttack;
            set => _canAttack = value;
        }
        
        [Inject]
        private void Construct(InputSystem_Actions input)
        {
            _input = input;
        }
        
        private void Awake()
        {
            _ammo = _config.MaxAmmo;
        }

        private void OnEnable()
        {
            _input.Player.Attack.performed += Attack;
            if(!_canAttack)
                Reload();
        }

        private void OnDisable()
        {
            _input.Player.Attack.performed -= Attack;
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = null;
        }

        public virtual void Attack(InputAction.CallbackContext callbackContext)
        {
            if (!_canAttack)
                return;
            if (_ammo == 0)
            {
                _coroutine = StartCoroutine(ReloadTimer());
                return;
            }
            _ammo -= 1;
        }
        
        public virtual void Reload()
        {
            _canAttack = false;
            _coroutine = StartCoroutine(ReloadTimer());
            _canAttack = true;
        }
        
        private IEnumerator ReloadTimer()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_config.ReloadTime);
            _ammo = _config.MaxAmmo;
            _canAttack = true;
            _coroutine = null;
        }
        
        public IEnumerator TimerToNextShoot()
        {
            CanAttack = false;
            yield return new WaitForSeconds(_config.TimeToNextShoot);
            CanAttack = true;
        }
        
        public abstract void Raycasting();
    }
}