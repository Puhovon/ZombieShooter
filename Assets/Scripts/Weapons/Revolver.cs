using Configs;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Weapons.Abstraction;
using Zenject;

namespace Weapons
{
    public class Revolver : Weapon
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private WeaponView _view;

        private IRaycaster _raycaster;
        private InputSystem_Actions _input;
        private Coroutine _coroutine;
        private bool _isAttacked;


        [Inject]
        private void Construct(InputSystem_Actions input)
        {
            _input = input;
        }
        
        private void Start()
        {
            _raycaster = new DefaultRaycaster(transform, Config.Distance, _particle, _view);
        }

        private void OnEnable()
        {
            OnAmmoChanged?.Invoke(Ammo);
            _input.Player.Attack.performed += HandleAttack;
        }

        private void OnDisable()
        {
            _input.Player.Attack.performed -= HandleAttack;
        }
        
        private void HandleAttack(InputAction.CallbackContext callbackContext)
        {
            if(IsCanAttack())
                Shoot();
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

        public override void Shoot()
        {
            Raycasting();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position,transform.forward * Config.Distance);
        }
    }
}