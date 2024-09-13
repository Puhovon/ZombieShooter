using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Weapons.Abstraction;

namespace Weapons
{
    public class Revolver : Weapon
    {
        [SerializeField] private ParticleSystem _particle;
        private DefaultRaycaster _raycaster;
        private Coroutine _coroutine;
        private void Start()
        {
            _raycaster = new DefaultRaycaster(transform, Config.Distance, _particle);
        }

        public override void Attack(InputAction.CallbackContext callbackContext)
        {
            base.Attack(callbackContext);
            if (Ammo == 0)
                return;
            Raycasting();
            
        }

        public override void Raycasting()
        {
            _raycaster.RayCasting(Config.Damage);
            _coroutine = StartCoroutine(TimerToNextShoot());
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position,transform.forward * Config.Distance);
        }
    }
}