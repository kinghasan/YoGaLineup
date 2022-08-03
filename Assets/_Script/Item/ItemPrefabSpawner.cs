using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;

public class ItemPrefabSpawner : ItemBase<Player>
{
    public override void Init()
    {
        base.Init();

        SubItems = transform.GetComponentsInChildren<ItemBase>(true).ToList();
        foreach (var item in SubItems)
        {
            if (item == this)
            {
                continue;
            }

            item.Init();
        }
    }

    public override void InitRenderer()
    {
        base.InitRenderer();
    }

    public override void OnTargetEffect(Player target)
    {
        
    }
}
