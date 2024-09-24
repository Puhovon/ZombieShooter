using System;
using System.Linq;
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

        public IDamagable Overlapping()
        {
            Collider[] colliders = new Collider[32];
            Collider res;
            IDamagable damagable;
            int finded = Physics.OverlapSphereNonAlloc(_center.position, _radius, colliders, _mask);
            res = colliders.FirstOrDefault(c => c.TryGetComponent(out damagable));
            if (res is null)
                throw new ArgumentNullException("Damagable not found");
            return res.GetComponent<IDamagable>();
            // TODO: Check why it's return to similar objects(two Player object)
            // for (int i = 0; i < finded; i++)
            // {
            //     if(colliders[i].TryGetComponent(out IDamagable damagable))
            //         damagable.TakeDamage(_damage);
            // }
        }
    }
}