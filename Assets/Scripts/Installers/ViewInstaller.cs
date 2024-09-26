using UnityEngine;
using Zenject;

public class ViewInstaller : MonoInstaller
{
    [SerializeField] private AmmoViewModel _ammoView;
    public override void InstallBindings()
    {
        Container.Bind<AmmoViewModel>().FromInstance(_ammoView);
    }
}