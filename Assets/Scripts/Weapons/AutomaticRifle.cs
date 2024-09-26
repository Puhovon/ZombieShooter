using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Weapons.Abstraction;

namespace Weapons
{
    public class AutomaticRifle : Weapon
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private WeaponView _view;
        private DefaultRaycaster _raycaster;
        private Coroutine _coroutine;
        private bool _isAttacked;
        
        private void Start()
        {
            _raycaster = new DefaultRaycaster(transform, Config.Distance, _particle, _view);
        }

        private void OnEnable()
        {
            OnAmmoChanged?.Invoke(Ammo);
            Input.Player.Attack.performed += SetShooting;
            Input.Player.Attack.canceled += SetShooting;
        }

        private void OnDisable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }   
        }

        private void Update()
        {
            if(_isAttacked)
                Shoot();
        }

        private void SetShooting(InputAction.CallbackContext context)
        {
            _isAttacked = context.ReadValue<float>() == 1;
            
        }
        
        public override void Shoot()
        { 
            if(Attack())
                Raycasting();
        }

        protected override void Raycasting()
        {
            if (Ammo > 0)
            {
                _coroutine = StartCoroutine(TimerToNextShoot());
            }
            else
            {
                _coroutine = StartCoroutine(TimerReload());
            }
            _raycaster.RayCasting(Config.Damage);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position,transform.forward * Config.Distance);
        }
    }
}