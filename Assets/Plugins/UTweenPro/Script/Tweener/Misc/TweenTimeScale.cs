using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Time Scale", "Misc", "GameManager Icon")]
    [Serializable]
    public partial class TweenTimeScale : TweenValueFloat<UnityEngine.Object>
    {
        public override bool SupportTarget => false;

        public override float Value
        {
            get => Time.timeScale;
            set
            {
                if (!Application.isPlaying) return;
                Time.timeScale = value;
            }
        }
    }

#if UNITY_EDITOR

    public partial class TweenTimeScale : TweenValueFloat<UnityEngine.Object>
    {
        public override void DrawFromToValue()
        {
            if (Data.IsPlaying && !Application.isPlaying)
            {
                GUIUtil.DrawTipArea(UTweenEditorSetting.Ins.ErrorColor, "Only effective at runtime");
            }

            if (Data.TimeMode != TimeMode.UnScaled && (Mathf.Abs(From) <= 1e-6 || Mathf.Abs(To) <= 1e-6))
            {
                GUIUtil.DrawTipArea(UTweenEditorSetting.Ins.ErrorColor, "From / To value is 0 and Time Mode is not UnScaled will cause the player stop running!");
            }

            base.DrawFromToValue();
        }
    }

#endif

    #region Extension

    public static partial class UTween
    {
        public static TweenTimeScale TimeScale(float to, float duration)
        {
            var tweener = Create<TweenTimeScale>()
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenTimeScale;
            return tweener;
        }

        public static TweenTimeScale TimeScale(float from, float to, float duration)
        {
            var tweener = Create<TweenTimeScale>()
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenTimeScale;
            return tweener;
        }
    }

    #endregion
}