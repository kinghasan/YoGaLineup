using System;
using System.Collections;
using System.Collections.Generic;
using Aya.Data.Persistent;
using UnityEngine;

[CreateAssetMenu(fileName = "AvatarSetting", menuName = "Setting/Avatar Setting")]
public class AvatarSetting : StoreSetting<AvatarSetting, AvatarData>
{
    public List<GameObject> SelectedAvatarList => Datas[SelectIndex].Prefabs;

    public override void Init()
    {
        base.Init();
        
    }
}
