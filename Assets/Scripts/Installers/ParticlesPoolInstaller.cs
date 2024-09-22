using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ParticlesPoolInstaller : MonoInstaller
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private ParticlesPool _pool;
    [SerializeField] private List<ParticleSystem> _particles;
    
    public override void InstallBindings()
    {
        Container.Bind<ParticleSystem>().FromInstance(_particle);
        Container.Bind<List<ParticleSystem>>().FromInstance(_particles);
        Container.Bind<ParticlesPool>().FromInstance(_pool);
    }
}