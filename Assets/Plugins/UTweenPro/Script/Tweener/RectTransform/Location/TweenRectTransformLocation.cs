using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    public enum RectTransformLocationType
    {
        // 0
        CornerLeftAbove = 0,
        OuterLeftAbove = 1,
        OuterCenterAbove = 2,
        OuterRightAbove = 3,
        CornerRightAbove = 4,
        // 1
        OuterLeftTop = 5,
        InnerLeftTop = 6,
        InnerCenterTop = 7,
        InnerRightTop = 8,
        OuterRightTop = 9,
        // 2
        OuterLeftMiddle = 10,
        InnerLeftMiddle = 11,
        InnerCenterMiddle = 12,
        InnerRightMiddle = 13,
        OuterRightMiddle = 14,
        // 3
        OuterLeftBottom = 15,
        InnerLeftBottom = 16,
        InnerCenterBottom = 17,
        InnerRightBottom = 18,
        OuterRightBottom = 19,
        // 4
        CornerLeftUnder = 20,
        OuterLeftUnder = 21,
        OuterCenterUnder = 22,
        OuterRightUnder = 23,
        CornerRightUnder = 24,
    }

    [Tweener("Rect Transform Location", "RectTransform")]
    [Serializable]
    public partial class TweenRectTransformLocation : TweenValueFloat<RectTransform>
    {
        public static List<RectTransformLocationData> DataList = new List<RectTransformLocationData>()
        {
            // 0
            new RectTransformLocationData(RectTransformLocationType.CornerLeftAbove, 0f, 1f, 0f, 1f, 1f, 0f),
            new RectTransformLocationData(RectTransformLocationType.OuterLeftAbove, 0f, 1f, 0f, 1f, 0f, 0f),
            new RectTransformLocationData(RectTransformLocationType.OuterCenterAbove, 0.5f, 1f, 0.5f, 1f, 0.5f, 0f),
            new RectTransformLocationData(RectTransformLocationType.OuterRightAbove, 1f, 1f, 1f, 1f, 1f, 0f),
            new RectTransformLocationData(RectTransformLocationType.CornerRightAbove, 1f, 1f, 1f, 1f, 0f, 0f),
            // 1
            new RectTransformLocationData(RectTransformLocationType.OuterLeftTop, 0f, 1f, 0f, 1f, 1f, 1f),
            new RectTransformLocationData(RectTransformLocationType.InnerLeftTop, 0f, 1f, 0f, 1f, 0f, 1f),
            new RectTransformLocationData(RectTransformLocationType.InnerCenterTop, 0.5f, 1f, 0.5f, 1f, 0.5f, 1f),
            new RectTransformLocationData(RectTransformLocationType.InnerRightTop, 1f, 1f, 1f, 1f, 1f, 1f),
            new RectTransformLocationData(RectTransformLocationType.OuterRightTop, 1f, 1f, 1f, 1f, 0f, 1f),
            // 2
            new RectTransformLocationData(RectTransformLocationType.OuterLeftMiddle, 0f, 0.5f, 0f, 0.51f, 1f, 0.5f),
            new RectTransformLocationData(RectTransformLocationType.InnerLeftMiddle, 0f, 0.5f, 0f, 0.5f, 0f, 0.5f),
            new RectTransformLocationData(RectTransformLocationType.InnerCenterMiddle, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f),
            new RectTransformLocationData(RectTransformLocationType.InnerRightMiddle, 1f, 0.5f, 1f, 0.5f, 1f, 0.5f),
            new RectTransformLocationData(RectTransformLocationType.OuterRightMiddle, 1f, 0.5f, 1f, 0.5f, 0f, 0.5f),
            // 3
            new RectTransformLocationData(RectTransformLocationType.OuterLeftBottom, 0f, 0f, 0f, 0f, 1f, 0f),
            new RectTransformLocationData(RectTransformLocationType.InnerLeftBottom, 0f, 0f, 0f, 0f, 0f, 0f),
            new RectTransformLocationData(RectTransformLocationType.InnerCenterBottom, 0.5f, 0f, 0.5f, 0f, 0.5f, 0f),
            new RectTransformLocationData(RectTransformLocationType.InnerRightBottom, 1f, 0f, 1f, 0f, 1f, 0f),
            new RectTransformLocationData(RectTransformLocationType.OuterRightBottom, 1f, 0f, 1f, 0f, 0f, 0f),
            // 4
            new RectTransformLocationData(RectTransformLocationType.CornerLeftUnder, 0f, 0f, 0f, 0f, 1f, 1f),
            new RectTransformLocationData(RectTransformLocationType.OuterLeftUnder, 0f, 0f, 0f, 0f, 0f, 1f),
            new RectTransformLocationData(RectTransformLocationType.OuterCenterUnder, 0.5f, 0f, 0.5f, 0f, 0.5f, 1f),
            new RectTransformLocationData(RectTransformLocationType.OuterRightUnder, 1f, 0f, 1f, 0f, 1f, 1f),
            new RectTransformLocationData(RectTransformLocationType.CornerRightUnder, 1f, 0f, 1f, 0f, 0f, 1f),
        };

        public RectTransformLocationType Start;
        public RectTransformLocationType End;
        public bool AutoPosition;

        public RectTransformLocationData StartData { get; set; }
        public RectTransformLocationData EndData { get; set; }

        public override float Value
        {
            get => _value;
            set
            {
                _value = value;
                var (anchorMin, anchorMax, pivot) = SampleLocation(StartData, EndData, _value);
                Target.anchorMin = anchorMin;
                Target.anchorMax = anchorMax;
                Target.pivot = pivot;
                if (AutoPosition)
                {
                    Target.anchoredPosition = Vector2.zero;
                }
            }
        }

        private float _value;

        public override void PreSample()
        {
            base.PreSample();

            StartData = DataList[(int)Start];
            EndData = DataList[(int)End];
        }

        public (Vector2, Vector2, Vector2) SampleLocation(RectTransformLocationData start, RectTransformLocationData end, float factor)
        {
            var anchorMin = Vector3.Lerp(StartData.AnchorMin, EndData.AnchorMin, factor);
            var anchorMax = Vector3.Lerp(StartData.AnchorMax, EndData.AnchorMax, factor);
            var pivot = Vector3.Lerp(StartData.Pivot, EndData.Pivot, factor);
            return (anchorMin, anchorMax, pivot);
        }

        public override void Reset()
        {
            base.Reset();

            Start = RectTransformLocationType.OuterLeftMiddle;
            End = RectTransformLocationType.InnerCenterMiddle;
            AutoPosition = true;
        }
    }

#if UNITY_EDITOR

        public partial class TweenRectTransformLocation : TweenValueFloat<RectTransform>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty StartProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty EndProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty AutoPositionProperty;

        internal RectTransformLocationWindow RectTransformLocationWindow;

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            RectTransformLocationWindow = new RectTransformLocationWindow(this);
        }

        public override void DrawIndependentAxis()
        {
        }

        public override void DrawFromToValue()
        {
            var window = RectTransformLocationWindow;
            var popupWindowSize = window.GetWindowSize();
            using (GUIHorizontal.Create())
            {
                var nodeSize = window.NodeSize.x - 5f;
                var windowRect = new Rect
                {
                    position = Event.current.mousePosition
                };
                windowRect.y += EditorGUIUtility.singleLineHeight;

                GUILayout.Label(nameof(Start), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                var btnFromRect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.miniButton, GUILayout.Width(nodeSize), GUILayout.Height(nodeSize));
                var btnFrom = GUI.Button(btnFromRect, "");
                window.DrawNode(StartProperty, btnFromRect.Reduce(1f), false);
                GUILayout.FlexibleSpace();

                if (btnFrom)
                {
                    RectTransformLocationWindow.ShowProperty = StartProperty;
                    PopupWindow.Show(windowRect, RectTransformLocationWindow);
                }

                GUILayout.Label(nameof(End), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                var btnToRect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.miniButton, GUILayout.Width(nodeSize), GUILayout.Height(nodeSize));
                var btnTo = GUI.Button(btnToRect, "");
                window.DrawNode(EndProperty, btnToRect.Reduce(1f), false);
                GUILayout.FlexibleSpace();

                if (btnTo)
                {
                    RectTransformLocationWindow.ShowProperty = EndProperty;
                    PopupWindow.Show(windowRect, RectTransformLocationWindow);
                }
            }

            base.DrawFromToValue();
        }

        public override void DrawAppend()
        {
            base.DrawAppend();

            GUIUtil.DrawToggleButton(AutoPositionProperty);
        }
    }

#endif

    #region Extension

    public partial class TweenRectTransformLocation : TweenValueFloat<RectTransform>
    {
        public TweenRectTransformLocation SetStartLocationType(RectTransformLocationType fromLocationType)
        {
            Start = fromLocationType;
            return this;
        }

        public TweenRectTransformLocation SetEndLocationType(RectTransformLocationType toLocationType)
        {
            End = toLocationType;
            return this;
        }

        public TweenRectTransformLocation SetAutoPosition(bool autoPosition)
        {
            AutoPosition = autoPosition;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenRectTransformLocation Location(RectTransform rectTransform, RectTransformLocationType startLocationType, RectTransformLocationType endLocationType, float duration, bool autoPosition = true)
        {
            var tweener = Play<TweenRectTransformLocation, RectTransform, float>(rectTransform, 0f, 1f, duration)
                .SetStartLocationType(startLocationType)
                .SetEndLocationType(endLocationType)
                .SetAutoPosition(autoPosition)
                .SetCurrent2From() as TweenRectTransformLocation;
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenRectTransformLocation TweenLocation(this RectTransform rectTransform, RectTransformLocationType startLocationType, RectTransformLocationType endLocationType, float duration, bool autoPosition = true)
        {
            var tweener = UTween.Location(rectTransform, startLocationType, endLocationType, duration, autoPosition);
            return tweener;
        }
    }

    #endregion
}