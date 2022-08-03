using Aya.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlFollow : GameEntity
{
    //private Transform Target => Player.Render.RenderTrans;
    public float AnimationScale { get; set; }
    private GirlFollow Target;
    private Vector3 TransPos;
    public bool IsStart { get; set; }

    public void Init(float LengthForPlayer)
    {
        TransPos = Vector3.zero;
        TransPos.z = LengthForPlayer;
        IsStart = true;
        Target = null;
        Animator.speed = 0;
    }

    public void Init(float LengthForPlayer, GirlFollow target)
    {
        TransPos = Vector3.zero;
        TransPos.z = LengthForPlayer;
        Target = target;
        IsStart = true;
        Animator.speed = 0;
    }

    public void Run()
    {
        if (!IsStart)
            return;

        //var speed = Player.Move.MoveSpeed * Player.State.SpeedMultiply * DeltaTime;
        var pos = Level.Level.GetPositionY(Player.Move.PathFollower.Distance + TransPos.z);
        TransPos.y = pos.y;
        if (Target != null)
        {
            if (Mathf.Abs(Target.AnimationScale - AnimationScale) <= 0.001f)
            {
                AnimationScale = Target.AnimationScale;
            }
            else
            {
                //var lerpScale = Mathf.Clamp(0.9f - SlowPower, 0.1f, 0.9f);
                AnimationScale = Mathf.Lerp(AnimationScale, Target.AnimationScale, YoGaGirlSetting.Ins.GirlFlowScale * Time.deltaTime);
            }
        }
        else
        {
            if (Mathf.Abs(Player.Control.AnimationScale - AnimationScale) <= 0.005f)
            {
                AnimationScale = Player.Control.AnimationScale;
            }
            else
            {
                //var lerpScale = Mathf.Clamp(0.9f - SlowPower, 0.1f, 0.9f);
                AnimationScale = Mathf.Lerp(AnimationScale, Player.Control.AnimationScale, YoGaGirlSetting.Ins.GirlFlowScale * Time.deltaTime);
            }
        }
        RendererTrans.localPosition = TransPos;
        RendererTrans.SetPositionZ(pos.z);
        Animator.Play("Idle", 0, AnimationScale);

        if (Player.State.EnableRun)
        {
            //string yogaStr = Player.Control._yogaList[Player.Control._targetIndex];
            //if (!string.IsNullOrEmpty(CurrentClip))
                //Animator.ResetTrigger(CurrentClip);
            //Animator.SetTrigger(yogaStr);
            //CurrentClip = yogaStr;
            //Play(yogaStr);
        }
    }
}
