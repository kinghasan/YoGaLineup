using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Self Time Scale", "Misc", "cs Script Icon")]
    [Serializable]
    public partial class TweenSelfTimeScale : TweenValueFloat<UnityEngine.Object>
    {
        public override bool SupportTarget => false;

        public override float Value
        {
            get => Data.SelfScale;
            set => Data.SelfScale = value;
        }
    }

#if UNITY_EDITOR

    public partial class TweenSelfTimeScale : TweenValueFloat<UnityEngine.Object>
    {
        public override void DrawFromToValue()
        {
            if (Data.TimeMode != TimeMode.UnScaled && (Mathf.Abs(From) <= 1e-6 || Mathf.Abs(To) <= 1e-6))
            {
                GUIUtil.DrawTipArea(UTweenEditorSetting.Ins.ErrorColor, "From / To value is 0 and Time Mode is not UnScaled will cause the animation stop running!");
            }

            base.DrawFromToValue();
        }
    }

#endif
}