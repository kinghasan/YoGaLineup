using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Physics Gravity", "Physics", "GameManager Icon")]
    [Serializable]
    public partial class TweenGravity : TweenValueVector3<UnityEngine.Object>
    {
        public override bool SupportTarget => false;

        public override Vector3 Value
        {
            get => Physics.gravity;
            set => Physics.gravity = value;
        }

        public override void Reset()
        {
            base.Reset();
            From = Physics.gravity;
            To = Physics.gravity;
        }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenGravity Gravity(Vector3 to, float duration)
        {
            var tweener = Create<TweenGravity>()
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenGravity;
            return tweener;
        }

        public static TweenGravity Gravity(Vector3 from, Vector3 to, float duration)
        {
            var tweener = Create<TweenGravity>()
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenGravity;
            return tweener;
        }
    }

    #endregion
}