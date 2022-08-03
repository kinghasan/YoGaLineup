using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerRankCondition : ItemCondition
{
    public CompareType Type;
    public int Rank;

    public override bool CheckCondition(object target)
    {
        var player = target as Player;
        if (player == null) return false;
        return CheckValue(player.State.Rank, Rank, Type);
    }
}
