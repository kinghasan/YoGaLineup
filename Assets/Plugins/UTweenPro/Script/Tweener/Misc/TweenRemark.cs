using System;
using Object = UnityEngine.Object;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Remark", "Misc", "console.infoicon")]
    [Serializable]
    public partial class TweenRemark : TweenValueFloat<Object>
    {
        public override bool SupportTarget => false;
        public override bool SupportFromTo => false;
        public override bool SupportSetCurrentValue => false;

        public string Remark;

        public override float Value { get; set; }

        public override void Sample(float factor)
        {
        }

        public override void Reset()
        {
            base.Reset();
            Remark = null;
        }
    }

#if UNITY_EDITOR
    public partial class TweenRemark : TweenValueFloat<Object>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty RemarkProperty;

        public override void DrawFromToValue()
        {
        }

        public override void DrawProgressBar()
        {
        }

        public override void DrawDurationDelay()
        {
        }

        public override void DrawBody()
        {
            GUIUtil.DrawTextArea(RemarkProperty);
        }

        public override void DrawEaseCurve()
        {
        }

        public override void DrawAppend()
        {
        }

        public override GenericMenu CreateContextMenu()
        {
            return base.CreateContextMenu();
        }
    }
#endif
}
