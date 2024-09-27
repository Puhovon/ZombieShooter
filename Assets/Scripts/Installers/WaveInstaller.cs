using Enemies.EnemyWave.Factory;
using UnityEngine;
using Zenject;

public class WaveInstaller : MonoInstaller
{
    
    public override void InstallBindings()
    {
        Container.Bind<EnemyFactory>().FromNew().AsSingle().NonLazy();
    }
}