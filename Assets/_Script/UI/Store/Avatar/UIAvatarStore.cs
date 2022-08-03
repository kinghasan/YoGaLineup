using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;

public class UIAvatarStore : UiStoreWindow<UIAvatarStore, UIAvatarItem, AvatarData>
{
    public override List<AvatarData> DataSources => AvatarSetting.Ins.Datas;
    public Transform PreviewTrans;
    public GameObject PreviewInstance { get; set; }

    public override void Refresh()
    {
        base.Refresh();
        RefreshPreview();
    }

    public void RefreshPreview()
    {
        if (PreviewInstance != null)
        {
            GamePool.DeSpawn(PreviewInstance);
            PreviewInstance = null;
        }

        var currentData = AvatarSetting.Ins.CurrentSelectData;
        PreviewInstance = GamePool.Spawn(currentData.PreviewPrefab, PreviewTrans).gameObject;
        PreviewInstance.transform.ResetLocal();
    }
}
