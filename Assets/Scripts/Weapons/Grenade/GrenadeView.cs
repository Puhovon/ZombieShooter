using UnityEngine;

namespace Weapons
{
    public class GrenadeView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private GameObject _mesh;

        public void Explore()
        {
            _mesh.SetActive(false);
            _particle.Play();
        }
    }
}