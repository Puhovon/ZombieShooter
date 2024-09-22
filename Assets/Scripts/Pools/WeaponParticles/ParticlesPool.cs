using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ParticlesPool : MonoBehaviour
{
    private ParticleSystem _particlePrefab;
    private List<ParticleSystem> _particles;

    [Inject]
    private void Construct(ParticleSystem particlePrefab, List<ParticleSystem> particles)
    {
        _particlePrefab = particlePrefab;
        _particles = particles;
    }

    public GameObject GetObject()
    {
        var _current = _particles.FirstOrDefault(p => !p.gameObject.activeSelf);
        if(_current is null)
        {
            _particles.Add(Instantiate(_particlePrefab));
            _current = _particles[^1];
            _current.transform.parent = transform;
        }

        GameObject obj;
        (obj = _current.gameObject).SetActive(true);
        return obj;
    }
}
