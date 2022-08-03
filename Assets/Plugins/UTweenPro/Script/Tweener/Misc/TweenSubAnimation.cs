using System;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace Aya.TweenPro
{
    [Tweener("Sub Animation", "Misc", "cs Script Icon")]
    [Serializable]
    public partial class TweenSubAnimation : TweenValueFloat<UTweenAnimation>
    {
        public override float Value
        {
            get => Target.Data.NormalizedProgress;
            set
            {
                if (Target == Data.TweenAnimation) return;
                Target.Data.NormalizedProgress = value;
                Target.Data.Sample(Target.Data.NormalizedProgress);
            }
        }

        public override void PreSample()
        {
            base.PreSample();
            Target.Data.IsSubAnimation = true;
            Target.Data.PreSample();
        }

        public override void StopSample()
        {
            base.StopSample();
            Target.Data.StopSample();
        }
    }

#if UNITY_EDITOR

    public partial class TweenSubAnimation : TweenValueFloat<UTweenAnimation>
    {
        public override void InitParam(TweenData data, MonoBehaviour target)
        {
            base.InitParam(data, target);
            Target = null;
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            if (Target == Data.TweenAnimation && Target != null)
            {
                GUIUtil.DrawTipArea(UTweenEditorSetting.Ins.ErrorColor, "Can't control self!");
            }
        }
    }

#endif

}