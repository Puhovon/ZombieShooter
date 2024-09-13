using Global.Abstractions;
using UnityEngine;

namespace Utilities
{
    public class DefaultRaycaster
    {
        private Transform _center;
        private float _distance;
        private ParticleSystem _particles;

        public DefaultRaycaster(Transform center, float distance, ParticleSystem particles)
        {
            _center = center;
            _distance = distance;
            _particles = particles;
        }

        public void RayCasting(int damage)
        {
            RaycastHit hit;
            if (!Physics.Raycast(_center.position, _center.forward, out hit, _distance))
            {
                Debug.Log("MISS");
                return;
            }
            _particles.transform.position = hit.point;
            _particles.Play();
            if(!hit.transform.TryGetComponent(out IDamagable damagable))
                return;
            damagable.TakeDamage(damage);
        }
    }
}