using System.Collections;
using Configs;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody), typeof(GrenadeView))]
    public class Grenade : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Collider _collider;
        [SerializeField] private GrenadeView _view;
        [Space]
        [Header("Params")]
        [SerializeField] private int _force;
        [SerializeField] private GrenadeConfig _config;
        
        public void Throw()
        {
            ResetComponents();
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
            StartCoroutine(WaitToDisable());
        }

        private IEnumerator WaitToDisable()
        {
            yield return new WaitForSeconds(2);
            gameObject.SetActive(false);
        }
    }
}