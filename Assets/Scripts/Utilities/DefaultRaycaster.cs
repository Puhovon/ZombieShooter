using Global.Abstractions;
using UnityEngine;
using Weapons;

namespace Utilities
{
    public class DefaultRaycaster: IRaycaster
    {
        private Transform _center;
        private float _distance;
        private ParticleSystem _particles;
        private WeaponView _weaponView;

        public DefaultRaycaster(Transform center, float distance, ParticleSystem particles, WeaponView weaponView)
        {
            _center = center;
            _distance = distance;
            _particles = particles;
            _weaponView = weaponView;
        }

        public void RayCasting(int damage)
        {
            RaycastHit hit;
            if (!Physics.Raycast(_center.position, _center.forward, out hit, _distance))
            {
                Debug.Log("MISS");
                return;
            }
            _weaponView.Shoot(hit);
            if(!hit.transform.TryGetComponent(out IDamagable damagable))
                return;
            damagable.TakeDamage(damage);
        }
    }
}