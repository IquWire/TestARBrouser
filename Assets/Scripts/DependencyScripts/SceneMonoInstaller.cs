
using UnityEngine;
using Zenject;

public class SceneMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DataLoader>().AsSingle();
    }
}
