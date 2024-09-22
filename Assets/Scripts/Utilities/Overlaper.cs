using Global.Abstractions;
using UnityEngine;

namespace Utilities
{
    public class Overlaper
    {
        private Transform _center;
        private float _radius;
        private LayerMask _mask;
        private int _damage;
        
        public Overlaper(Transform center, float radius, LayerMask mask, int damage)
        {
            _center = center;
            _radius = radius;
            _mask = mask;
            _damage = damage;
        }

        public void Overlapping()
        {
            Collider[] colliders = new Collider[32];
            int finded = Physics.OverlapSphereNonAlloc(_center.position, _radius, colliders, _mask);
            for (int i = 0; i < finded; i++)
            {
                if(colliders[i].TryGetComponent(out IDamagable damagable))
                    damagable.TakeDamage(_damage);
            }
        }
    }
}