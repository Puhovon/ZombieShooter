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
            _input.Player.Attack.performed += HandleAttack;
        }

        private void OnDisable()
        {
            _input.Player.Attack.performed -= HandleAttack;
        }

        private void HandleAttack(InputAction.CallbackContext callbackContext)
        {
            if(Attack())
                Shoot();
        }

        protected override void Raycasting()
        {
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