using System;
using System.Collections.Generic;
using System.Linq;
using Global.Abstractions;
using NUnit.Framework;
using UnityEngine;

namespace Utilities
{
    public class Overlaper
    {
        private Transform _center;
        private float _radius;
        private LayerMask _mask;
        
        public Overlaper(Transform center, float radius, LayerMask mask)
        {
            _center = center;
            _radius = radius;
            _mask = mask;
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
        }

        public IDamagable[] OverlappingAll()
        {
            Collider[] colliders = new Collider[64];
            List<IDamagable> res = new List<IDamagable>();
            int finded = Physics.OverlapSphereNonAlloc(_center.position, _radius, colliders, _mask);
            for (int i = 0; i < finded; i++)
            {
                if(colliders[i].TryGetComponent(out IDamagable damagable))
                    res.Add(damagable);
            }

            return res.ToArray();
        }
    }
}