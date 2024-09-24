using System.Collections.Generic;
using Pools.Grenades;
using UnityEngine;
using Weapons;
using Zenject;

public class GrenadesPoolInstaller : MonoInstaller
{
    [SerializeField] private List<Grenade> _startPool;
    [SerializeField] private GrenadesPool _pool;
    public override void InstallBindings()
    {
        Container.Bind<List<Grenade>>().FromInstance(_startPool);
        Container.Bind<GrenadesPool>().FromInstance(_pool);
    }
}