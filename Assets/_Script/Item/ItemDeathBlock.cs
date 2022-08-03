using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeathBlock : ItemBase<Player>
{
    public override void OnTargetEffect(Player target)
    {
        target.Die();
    }
}
