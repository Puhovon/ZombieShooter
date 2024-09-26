using Configs;
using Global;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Health _playerHealth;
    public override void InstallBindings()
    {
        var input = new InputSystem_Actions();
        Container.Bind<InputSystem_Actions>().FromInstance(input).AsSingle();
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle().NonLazy();
        Container.Bind<Transform>().FromInstance(_playerTransform).NonLazy();
        Container.Bind<Health>().FromInstance(_playerHealth);
    }
}