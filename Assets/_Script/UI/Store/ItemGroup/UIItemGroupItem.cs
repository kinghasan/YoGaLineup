using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemGroupItem : UIStoreItemBase<ItemGroupData>
{
    public override void Refresh()
    {
        base.Refresh();
    }

    public override void Select()
    {
        base.Select();
        CurrentLevel.InitItemsRenderer();
    }
}
