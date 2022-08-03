using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ItemChangeGamePhase : ItemBase<Player>
{
    [BoxGroup("Game Phase")] public GamePhaseType GamePhase;
    [BoxGroup("Game Phase")] public string[] Args;

    public override void OnTargetEffect(Player target)
    {
        if (!target.IsPlayer) return;
        Game.Enter(GamePhase, Args as object[]);
    }
}
