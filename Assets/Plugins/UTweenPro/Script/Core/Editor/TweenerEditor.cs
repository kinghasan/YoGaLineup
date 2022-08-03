#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aya.TweenPro
{
    public abstract partial class Tweener
    {
        [NonSerialized] public SerializedProperty TweenerProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty ActiveProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty DurationProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty DelayProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty HoldStartProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty HoldEndProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty EaseProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty StrengthProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty CurveProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty SpaceProperty;

        [TweenerProperty, NonSerialized] internal SerializedProperty FoldOutProperty = null;
        [TweenerProperty, NonSerialized] internal SerializedProperty DurationModeProperty = null;

        public Editor Editor => Data.Editor;
        public Object EditorTarget => Data.EditorTarget;
        public MonoBehaviour MonoBehaviour => Data.MonoBehaviour;
        public GameObject GameObject => Data.GameObject;
        public SerializedObject SerializedObject => Data.SerializedObject;

        [NonSerialized] public int Index = -1;

        public bool CanMoveDown => Index < Data.TweenerList.Count - 1;
        public bool CanMoveUp => Index > 0;

        public TweenerAttribute TweenerAttribute
        {
            get
            {
                if (_tweenerAttribute == null)
                {
                    _tweenerAttribute = TypeCaches.TweenerAttributeDic[GetType()];
                }

                return _tweenerAttribute;
            }
        }

        private TweenerAttribute _tweenerAttribute;

        public TweenGroupData TweenGroupData
        {
            get
            {
                if (_tweenGroupData == null)
                {
                    _tweenGroupData = UTweenEditorSetting.Ins.GetGroupData(TweenerAttribute.Group);
                }

                return _tweenGroupData;
            }
        }

        private TweenGroupData _tweenGroupData;

        public bool IsProgressFull => Math.Abs(Duration - Data.Duration) <= 1e-6f && Math.Abs(Delay) <= 1e-6f;

        public bool ShowProgressBar
        {
            get
            {
                if (Data.SingleMode) return false;
                if (FoldOut) return true;
                if (!Active) return false;
                if (Data.IsPlaying) return true;
                if (IsProgressFull && UTweenEditorSetting.Ins.HideFullSubProgress) return false;
                return true;
            }
        }

        private static Tweener _clipboard;

        public virtual void InitParam(TweenData data, MonoBehaviour target)
        {
            Data = data;
            Duration = Data.Duration;
        }

        public virtual void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            Index = index;
            Data = data;
            TweenerProperty = tweenerProperty;

            CacheDurationDelayProperty();
            TweenerPropertyAttribute.CacheProperty(this, TweenerProperty);

            _clearEaseCurvePreview();
        }

        internal virtual void CacheDurationDelayProperty()
        {
            DurationProperty = TweenerProperty.FindPropertyRelative(nameof(Duration));
            DelayProperty = TweenerProperty.FindPropertyRelative(nameof(Delay));
        }

        public virtual void DrawTweener()
        {
            using (GUIFoldOut.Create(FoldOutProperty, DrawTweenerHeader, DrawProgressBar))
            {
                if (Data.SpeedBased && !SupportSpeedBased)
                {
                    GUIUtil.DrawTipArea(UTweenEditorSetting.Ins.ErrorColor, "Not Support Speed Based!");
                }

                if (FoldOut)
                {
                    using (GUIEnableArea.Create(ActiveProperty.boolValue && !Data.IsInProgress))
                    {
                        if (SupportTarget) DrawTarget();
                        if (SupportIndependentAxis) DrawIndependentAxis();
                        if (SupportFromTo) DrawFromToValue();

                        DrawBody();
                        DrawDurationDelay();
                        DrawEaseCurve();
                        DrawAppend();

                        // if (Data.Mode == TweenEditorMode.Component && SupportOnUpdate && ShowEvent)
                        // {
                        //     DrawEvent();
                        // }
                    }

                }
            }

            DrawGroupReminder();
        }

        public virtual void DrawGroupReminder()
        {
            if (!UTweenEditorSetting.Ins.ShowGroupReminder) return;
            var color = TweenGroupData.Color;
            var width = UTweenEditorSetting.Ins.GroupReminderWidth;
            var headerHeight = EditorGUIUtility.singleLineHeight + (ShowProgressBar ? 10 : 6);
            var rect = GUILayoutUtility.GetLastRect();

            rect.x -= width + 2;
            rect.width = width;
            var headerRect = rect;
            headerRect.height = headerHeight;
            EditorGUI.DrawRect(headerRect, color);
            var bodyRect = rect;
            rect.y += headerHeight;
            rect.height -= headerHeight;
            EditorGUI.DrawRect(bodyRect, color * 0.6f);
        }

        public virtual void DrawTweenerHeader()
        {
            using (GUIHorizontal.Create())
            {
                // Icon
                DrawHeaderIcon();
                GUILayout.Space(2);

                // Active Toggle
                ActiveProperty.boolValue = EditorGUILayout.Toggle(ActiveProperty.boolValue, GUILayout.Width(EditorStyle.CharacterWidth));
                GUILayout.Space(2);

                // Title
                using (GUIHorizontal.Create())
                {
                    DrawTitle();
                }

                var btnTitleRect = GUILayoutUtility.GetLastRect();
                var btnTweenerName = GUI.Button(btnTitleRect, GUIContent.none, EditorStyles.label);
                if (btnTweenerName)
                {
                    FoldOutProperty.boolValue = !FoldOutProperty.boolValue;
                }

                // Menu Button
                var btnContextMenu = GUIUtil.DrawContextMenuButton();
                if (btnContextMenu)
                {
                    var menu = CreateContextMenu();
                    menu.ShowAsContext();
                }
            }
        }

        public virtual void DrawTitle()
        {
            using (GUIEnableArea.Create(Active, false))
            {
                var name = TweenerAttribute.DisplayName;
                GUILayout.Label(name, EditorStyles.boldLabel);
            }
        }

        public virtual void DrawHeaderIcon()
        {

        }

        public virtual void DrawProgressBar()
        {
            if (!ShowProgressBar) return;
            using (GUIHorizontal.Create())
            {
                GUILayout.Space(EditorStyle.CharacterWidth);
                var currentHeight = FoldOut ? 4f : 2f;
                using (GUIEnableArea.Create(Active, false))
                {
                    var normalized = Data.EditorNormalizedProgress;
                    if (!Active) normalized = 0f;
                    GUIUtil.DrawDraggableProgressBar(SerializedObject.targetObject, currentHeight,
                        DurationFromNormalized, DurationToNormalized, normalized,
                        HoldStartProperty.boolValue, HoldEndProperty.boolValue,
                        (from, to) =>
                        {
                            if (Data.IsPlaying) return;
                            DurationFromNormalized = from;
                            DurationToNormalized = to;
                        });
                }

                GUILayout.Space(EditorStyle.CharacterWidth);
            }

            if (FoldOut)
            {
                GUILayout.Space(3);
                if (!IsProgressFull)
                {
                    using (GUIHorizontal.Create())
                    {
                        GUIUtil.DrawToggleButton(HoldStartProperty);
                        GUIUtil.DrawToggleButton(HoldEndProperty);
                    }
                }
                else
                {
                    HoldStartProperty.boolValue = false;
                    HoldEndProperty.boolValue = false;
                }
            }
        }

        public virtual void DrawTarget()
        {

        }

        public virtual void DrawIndependentAxis()
        {

        }

        public virtual void DrawFromToValue()
        {

        }

        public virtual void DrawDurationDelay()
        {
            using (GUIHorizontal.Create())
            {
                if (Data.SingleMode)
                {
                    DurationProperty.floatValue = Data.Duration;
                    DelayProperty.floatValue = 0f;
                    return;
                }

                var durationChanged = false;
                var delayChanged = false;

                if (DurationMode == DurationMode.DurationDelay)
                {
                    using (var check = GUICheckChangeArea.Create())
                    {
                        DurationProperty.floatValue = (float)Math.Round(EditorGUILayout.FloatField(nameof(Duration), DurationProperty.floatValue), 3);
                        durationChanged = check.Changed;
                    }

                    using (var check = GUICheckChangeArea.Create())
                    {
                        DelayProperty.floatValue = (float)Math.Round(EditorGUILayout.FloatField(nameof(Delay), DelayProperty.floatValue), 3);
                        delayChanged = check.Changed;
                    }
                }

                if (DurationMode == DurationMode.FromTo)
                {
                    GUILayout.Label("Time", EditorStyles.label, GUILayout.Width(EditorGUIUtility.labelWidth));
                    using (GUILabelWidthArea.Create(EditorStyle.CharacterWidth))
                    {
                        using (var check = GUICheckChangeArea.Create())
                        {
                            DelayProperty.floatValue = (float)Math.Round(GUIUtil.DrawFloatProperty("F", DelayProperty.floatValue, true), 3);
                            delayChanged = check.Changed;
                        }

                        using (var check = GUICheckChangeArea.Create())
                        {
                            DurationProperty.floatValue = (float)Math.Round(GUIUtil.DrawFloatProperty("T", DelayProperty.floatValue + DurationProperty.floatValue, true) - DelayProperty.floatValue, 3);
                            durationChanged = check.Changed;
                        }
                    }
                }

                GUIUtil.DrawSelectEnumButton(DurationModeProperty, typeof(DurationMode));

                if (durationChanged)
                {
                    if (DurationProperty.floatValue < 0) DurationProperty.floatValue = 0;
                    else if (DurationProperty.floatValue + DelayProperty.floatValue > Data.DurationProperty.floatValue)
                    {
                        DurationProperty.floatValue = Data.DurationProperty.floatValue - DelayProperty.floatValue;
                    }
                }

                if (delayChanged)
                {
                    if (DelayProperty.floatValue < 0) DelayProperty.floatValue = 0;
                    else if (DurationProperty.floatValue + DelayProperty.floatValue > Data.DurationProperty.floatValue)
                    {
                        DelayProperty.floatValue = Data.DurationProperty.floatValue - DurationProperty.floatValue;
                    }
                }
            }
        }

        public virtual void DrawEaseCurve()
        {
            using (GUIHorizontal.Create())
            {
                // Ease
                GUILayout.Label(nameof(Ease), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                var displayEaseName = EaseType.FunctionInfoDic[Ease].DisplayName;
                var easeRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight, EditorStyles.popup);
                var easeTypeBtn = GUI.Button(easeRect, displayEaseName, EditorStyles.popup);
                if (easeTypeBtn)
                {
                    var menu = CreateEaseTypeMenu();
                    menu.ShowAsContext();
                }

                // Curve
                GUILayout.Label(nameof(Curve), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                // var curveRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight, EditorStyles.popup);
                if (IsCustomCurve)
                {
                    CurveProperty.animationCurveValue = EditorGUILayout.CurveField(CurveProperty.animationCurveValue);
                }
                else
                {
                    using (GUIEnableArea.Create(false))
                    {
                        _cacheEaseCurve(CacheEaseFunction);
                        EditorGUILayout.CurveField(_cacheCurve); //, GUILayout.Height(EditorStyle.SingleButtonWidth * 2f));
                    }
                }

                if (IsCustomCurve)
                {
                    if (GUIUtil.DrawSelectModeButton())
                    {
                        var menu = CreateCurveMenu();
                        menu.ShowAsContext();
                    }
                }
            }

            // Strength
            if (CacheEaseFunction.SupportStrength)
            {
                using (GUIHorizontal.Create())
                {
                    GUILayout.Label(nameof(Strength), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                    StrengthProperty.floatValue = GUILayout.HorizontalSlider(StrengthProperty.floatValue, 0f, 1f);
                }
            }
        }

        public virtual GenericMenu CreateCurveMenu()
        {
            var menu = new GenericMenu();
            menu.AddItem("Reset Curve", false, () =>
            {
                CurveProperty.animationCurveValue = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));
                CurveProperty.serializedObject.ApplyModifiedProperties();
            });

            menu.AddItem("Reverse Curve", false, () =>
            {
                CurveProperty.animationCurveValue = CurveProperty.animationCurveValue.Reverse();
                CurveProperty.serializedObject.ApplyModifiedProperties();
            });

            return menu;
        }

        private AnimationCurve _cacheCurve;
        private int _cacheType;
        private float _cacheStrength;

        private void _clearEaseCurvePreview()
        {
            _cacheCurve = null;
            _cacheType = -1;
            _cacheStrength = 1f;
        }

        private void _cacheEaseCurve(EaseFunction easeFunction)
        {
            if (_cacheType == easeFunction.Type && Math.Abs(_cacheStrength - Strength) > 1e-6f && _cacheCurve != null) return;
            _cacheType = easeFunction.Type;
            _cacheStrength = Strength;
            var step = 0.01f;
            _cacheCurve = new AnimationCurve();
            for (var i = 0f; i <= 1f; i += step)
            {
                var j = easeFunction.Ease(0f, 1f, i, Strength);
                _cacheCurve.AddKey(i, j);
            }
        }

        internal GenericMenu CreateEaseTypeMenu()
        {
            var menu = new GenericMenu();
            foreach (var kv in EaseType.FunctionInfoDic)
            {
                var easeType = kv.Key;
                var easeFunctionAttribute = kv.Value;
                menu.AddItem(easeFunctionAttribute.MenuPath, Ease == easeType, () =>
                {
                    var easeFunction = EaseType.FunctionDic[easeType];
                    EaseProperty.intValue = easeType;
                    StrengthProperty.floatValue = easeFunction.SupportStrength ? easeFunction.DefaultStrength : 1f;
                    EaseProperty.serializedObject.ApplyModifiedProperties();
                });

                if (easeType < 0)
                {
                    menu.AddSeparator("");
                }
            }

            return menu;
        }

        public virtual void DrawBody()
        {
        }

        public virtual void DrawEvent()
        {
        }

        public virtual void DrawAppend()
        {
            if (SupportSpace)
            {
                GUIUtil.DrawToolbarEnum(SpaceProperty, nameof(Space), typeof(SpaceMode));
            }
        }

        #region Context Menu

        public void MoveUp()
        {
            if (!CanMoveUp) return;
            Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Move Up");
            (Data.TweenerList[Index - 1], Data.TweenerList[Index]) = (Data.TweenerList[Index], Data.TweenerList[Index - 1]);
        }

        public void MoveDown()
        {
            if (!CanMoveDown) return;
            Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Move Down");
            (Data.TweenerList[Index + 1], Data.TweenerList[Index]) = (Data.TweenerList[Index], Data.TweenerList[Index + 1]);
        }

        public virtual GenericMenu CreateContextMenu()
        {
            var menu = new GenericMenu();

            // Reset
            menu.AddItem(new GUIContent("Reset"), false, () =>
            {
                Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Reset Tweener");
                Reset();
            });

            // Remove
            menu.AddItem(new GUIContent("Remove Tweener"), false, () =>
            {
                Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Remove Tweener");
                Data.TweenerList.RemoveAt(Index);
                OnRemoved();
            });
            menu.AddSeparator("");

            // Move Up / Move Down / Auto Sort
            menu.AddItem(CanMoveUp, "Move Up", false, MoveUp);

            menu.AddItem(CanMoveDown, "Move Down", false, MoveDown);

            if (Data.TweenerList.Count > 1)
            {
                menu.AddItem(true, "Auto Sort", false, () =>
                {
                    Data.AutoSortTweener();
                });
            }

            menu.AddSeparator("");

            // Copy / Paste
            menu.AddItem("Copy Tweener", false, () =>
            {
                _clipboard = (Tweener)Activator.CreateInstance(GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(this, _clipboard);
            });
            var canPasteAsNew = _clipboard != null;
            menu.AddItem(canPasteAsNew, "Paste Tweener As New", false, () =>
            {
                Undo.RecordObject(SerializedObject.targetObject, "Paste Tweener As New");
                var tweener = (Tweener)Activator.CreateInstance(_clipboard.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(_clipboard, tweener);
                Data.AddTweener(tweener);
            });
            var canPasteValues = _clipboard != null && _clipboard.GetType() == GetType();
            menu.AddItem(canPasteValues, "Paste Tweener Values", false, () =>
            {
                Undo.RecordObject(SerializedObject.targetObject, "Paste Tweener Values");
                EditorUtility.CopySerializedManagedFieldsOnly(_clipboard, this);
            });
            menu.AddSeparator("");

            // Show / Hide Event
            // if (SupportOnUpdate && Data.Mode == TweenEditorMode.Component)
            // {
            //     menu.AddItem(ShowEvent ? "Hide Callback Event" : "Show Callback Event", false, () =>
            //     {
            //         Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Switch Event");
            //         ShowEvent = !ShowEvent;
            //         if (!ShowEvent) ResetCallback();
            //     });
            // }

            // Curve
            
            return menu;
        }

        #endregion
    }
}
#endif