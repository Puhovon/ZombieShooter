using UnityEngine;
using Zenject;

namespace Weapons
{
    public class WeaponView : MonoBehaviour
    {
        private ParticlesPool _particlesPool;
        
        [Inject]
        private void Construct(ParticlesPool particles)
        {
            _particlesPool = particles;
        }

        public void Shoot(RaycastHit hit)
        {
            var obj = _particlesPool.GetObject();
            obj.transform.position = hit.point;
            obj.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }
}