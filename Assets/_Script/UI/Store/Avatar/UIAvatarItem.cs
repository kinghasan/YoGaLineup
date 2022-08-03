using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAvatarItem : UIStoreItemBase<AvatarData>
{
    public override void Refresh()
    {
        base.Refresh();
    }

    public override void Select()
    {
        base.Select();
        Player.Init();
    }
}
