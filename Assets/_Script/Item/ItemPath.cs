using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using Aya.TweenPro;
using Sirenix.OdinInspector;
using UnityEngine;

public class ItemPath : ItemBase<Player>
{
    [BoxGroup("Path")] public bool SwitchPath;
    [BoxGroup("Path"), ShowIf("SwitchPath")] public int SwitchPathIndex;

    [BoxGroup("Path")] public bool LimitRange;
    [BoxGroup("Path"), ShowIf("LimitRange")] public Vector2 Range;

    [BoxGroup("Path")] public bool KeepDirection;

    [BoxGroup("Path")] public bool GoToCenter;
    [BoxGroup("Path"), ShowIf("GoToCenter")] public float GoToCenterDuration = 0.5f;

    [BoxGroup("Path")] public bool EnableInput = true;

    [BoxGroup("Path")] public bool ChangeSpeed = false;
    [BoxGroup("Path"), ShowIf("ChangeSpeed")] public float SpeedMultiply = 1f;

    public LevelBlock Block { get; set; }
    public int BlockIndex { get; set; }

    public override void Init()
    {
        base.Init();
        Block = GetComponentInParent<LevelBlock>();
        BlockIndex = CurrentLevel.BlockInsList.IndexOf(Block);
    }

    public override void OnTargetEffect(Player target)
    {
        target.State.EnableInput = EnableInput;

        if (ChangeSpeed)
        {
            target.State.SpeedMultiply = SpeedMultiply;
        }

        if (SwitchPath )
        {
            target.Move.SwitchPath(BlockIndex, SwitchPathIndex);
        }

        target.State.LimitTurnRange = LimitRange;
        if (LimitRange)
        {
            target.State.TurnRange = Range;
        }
        else
        {
            target.State.TurnRange = target.Move.CurrentPath.TurnRange;
        }

        if (GoToCenter)
        {
            UTween.Value(target.Render.RenderTrans.localPosition.x, 0f, GoToCenterDuration, value =>
            {
                target.Render.RenderTrans.SetLocalPositionX(value);
            });
        }

        target.State.KeepDirection = KeepDirection;
    }
}
