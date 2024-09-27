using Pools.Grenades;
using UnityEngine;

namespace Weapons
{
    public class GrenadeView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private GameObject _mesh;

        public void Explore()
        {
            _particle.transform.position = _mesh.transform.position;
            _mesh.SetActive(false);
            _particle.Play();
        }

        public void Disable(GrenadesPool grenadesPool)
        {
            transform.parent = grenadesPool.transform;
            transform.localPosition = ZeroVector3;
            _mesh.transform.localPosition = ZeroVector3;
            _mesh.transform.localRotation = ZeroQuaternion;
            transform.localRotation = ZeroQuaternion;
            gameObject.SetActive(false);
        }
        
        private Quaternion ZeroQuaternion => new Quaternion(0, 0, 0, 0);
        private Vector3 ZeroVector3 => new Vector3(0, 0, 0);
    }
}