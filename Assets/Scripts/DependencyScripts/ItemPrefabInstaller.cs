using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemPrefabInstaller : MonoInstaller
{
    [SerializeField]
    private ItemUI ItemUiPrefab;

    public override void InstallBindings()
    {
        Container.BindInstance(ItemUiPrefab).AsTransient();
    }
}
