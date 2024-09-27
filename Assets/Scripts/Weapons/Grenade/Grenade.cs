using System;
using System.Collections;
using Configs;
using Pools.Grenades;
using UnityEngine;
using Utilities;
using Zenject;

namespace Weapons
{
    [RequireComponent(typeof(GrenadeView))]
    public class Grenade : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private GrenadesPool _pool;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Collider _collider;
        [SerializeField] private GrenadeView _view;
        [Space]
        [Header("Params")]
        [SerializeField] private int _force;
        [SerializeField] private GrenadeConfig _config;
        [SerializeField] private LayerMask mask;

        private Overlaper _overlaper;
        
        [Inject]
        private void Construct(GrenadesPool pool) => _pool = pool;

        private void Start()
        {
            _overlaper = new Overlaper(_rb.transform, _config.radius, mask);
        }

        public void Throw()
        {
            ResetComponents();
            _rb.gameObject.SetActive(true);
            _rb.AddForce(transform.forward * _force, ForceMode.Impulse);
            StartCoroutine(TimerToExplosion());
        }

        private void ResetComponents()
        {
            transform.parent = null;
            _rb.isKinematic = false;
            _collider.isTrigger = false;
        }

        private IEnumerator TimerToExplosion()
        {
            yield return new WaitForSeconds(_config.timeBeforeExplore);
            _view.Explore();
            var finded =_overlaper.OverlappingAll();
            foreach(var f in finded)
                f.TakeDamage(_config.damage);
            
            StartCoroutine(WaitToDisable());
        }

        private IEnumerator WaitToDisable()
        {
            yield return new WaitForSeconds(2);
            _rb.isKinematic = true;
            _collider.isTrigger = true;
            _view.Disable(_pool);
        }
    }
}