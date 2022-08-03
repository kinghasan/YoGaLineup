using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class AvatarData : StoreData
{
    public new AvatarSetting Setting => base.Setting as AvatarSetting;
    public override int Cost => Setting.Costs[Setting.BuyCount];
    public override UnlockMode UnlockMode => UnlockMode.FixedValue;
    public override int UnlockValue => Setting.UnlockValues[0];

    public GameObject Prefab;
    public GameObject PreviewPrefab;

    [TableColumnWidth(100)]
    public List<GameObject> Prefabs;

    public override void Init()
    {
        base.Init();
    }
}
