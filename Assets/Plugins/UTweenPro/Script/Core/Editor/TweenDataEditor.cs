#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aya.TweenPro
{
    public partial class TweenData
    {
        [NonSerialized] public TweenEditorMode Mode;
        [NonSerialized] public Editor Editor;
        [NonSerialized] public SerializedProperty TweenDataProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty IdentifierProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty TweenerListProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty DurationProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty DelayProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty BackwardProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty PlayModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty PlayCountProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty AutoPlayProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty UpdateModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty IntervalProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty Interval2Property;
        [TweenerProperty, NonSerialized] public SerializedProperty TimeModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty SelfScaleProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty PreSampleModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty AutoKillProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty SpeedBasedProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty OnPlayProperty = null;
        [TweenerProperty, NonSerialized] public SerializedProperty OnStartProperty = null;
        [TweenerProperty, NonSerialized] public SerializedProperty OnLoopStartProperty = null;
        [TweenerProperty, NonSerialized] public SerializedProperty OnLoopEndProperty = null;
        [TweenerProperty, NonSerialized] public SerializedProperty OnUpdateProperty = null;
        [TweenerProperty, NonSerialized] public SerializedProperty OnPauseProperty = null;
        [TweenerProperty, NonSerialized] public SerializedProperty OnResumeProperty = null;
        [TweenerProperty, NonSerialized] public SerializedProperty OnStopProperty = null;
        [TweenerProperty, NonSerialized] public SerializedProperty OnCompleteProperty = null;

        [TweenerProperty, NonSerialized] internal SerializedProperty FoldOutProperty = null;
        [TweenerProperty, NonSerialized] internal SerializedProperty FoldOutEventProperty = null;
        [TweenerProperty, NonSerialized] internal SerializedProperty EnableIdentifierProperty = null;
        [TweenerProperty, NonSerialized] internal SerializedProperty EventTypeProperty = null;

        [NonSerialized] public float EditorNormalizedProgress;
        [NonSerialized] public bool PreviewSampled = false;

        public Object EditorTarget => Editor.target;
        public MonoBehaviour MonoBehaviour => EditorTarget as MonoBehaviour;
        public GameObject GameObject => EditorTarget as GameObject;
        public SerializedObject SerializedObject => Editor.serializedObject;

        public void InitEditor(TweenEditorMode mode, Editor editor)
        {
            Mode = mode;
            Editor = editor;
            PreviewSampled = false;

            TweenDataProperty = SerializedObject.FindProperty("Data");
            TweenerPropertyAttribute.CacheProperty(this, TweenDataProperty);

            foreach (var tweener in TweenerList)
            {
                tweener.Index = -1;
            }

            if (Mode == TweenEditorMode.Component)
            {
                RecordObject();
            }
        }

        public virtual void OnInspectorGUI()
        {
            using (GUIWideMode.Create(true))
            {
                using (GUILabelWidthArea.Create(EditorStyle.LabelWidth))
                {
                    DrawProgressBar();
                    using (GUIEnableArea.Create(!IsInProgress))
                    {
                        DrawTweenData();

                        if (SerializedObject.isEditingMultipleObjects)
                        {
                            SerializedObject.ApplyModifiedProperties();
                            return;
                        }

                        DrawTweenerList();

                        DrawAppend();
                        DrawEvent();
                    }
                }
            }
        }

        #region Draw Progress Bar
      
        public virtual void DrawProgressBar()
        {
            if (Mode == TweenEditorMode.ScriptableObject) return;
            using (GUIGroup.Create())
            {
                using (GUIHorizontal.Create())
                {
                    var progressBarHeight = EditorGUIUtility.singleLineHeight;
                    var isInProgress = IsInProgress;
                    using (GUIColorArea.Create(UTweenEditorSetting.Ins.ProgressColor, isInProgress))
                    {
                        var btnContent = isInProgress ? EditorStyle.PlayButtonOn : EditorStyle.PlayButton;
                        var btnPlay = GUILayout.Button(btnContent, EditorStyles.miniButtonMid, GUILayout.Width(EditorGUIUtility.singleLineHeight));
                        if (btnPlay)
                        {
                            if (!isInProgress)
                            {
                                RecordObject();
                                ControlMode = TweenControlMode.Component;
                                State = PlayState.None;
                                Play();
                            }
                            else
                            {
                                Stop();
                                RestoreObject();
                            }
                        }
                    }

                    if (isInProgress)
                    {
                        EditorNormalizedProgress = RuntimeNormalizedProgress;
                    }

                    GUIUtil.DrawDraggableProgressBar(SerializedObject.targetObject, progressBarHeight, EditorNormalizedProgress,
                        value =>
                        {
                            if (isInProgress) return;
                            if (!IsInitialized)
                            {
                                Initialize(true);
                            }

                            EditorNormalizedProgress = value;
                            try
                            {
                                Sample(EditorNormalizedProgress);
                            }
                            catch
                            {
                                //
                            }
                        });

                    if (!string.IsNullOrEmpty(Identifier))
                    {
                        var rect = GUILayoutUtility.GetLastRect();
                        GUI.Label(rect, Identifier, EditorStyles.centeredGreyMiniLabel);
                    }

                    using (GUIEnableArea.Create(!IsInProgress))
                    {
                        var btnDirection = GUILayout.Button(Backward ? "←" : "→", EditorStyles.miniButtonMid, GUILayout.Width(EditorGUIUtility.singleLineHeight));
                        if (btnDirection)
                        {
                            BackwardProperty.boolValue = !BackwardProperty.boolValue;
                        }
                    }
                }
            }
        }

        #endregion

        #region Draw TweenData

        private float _originalDuration;
        private bool _durationChanged;

        public void DrawTweenData()
        {
            _originalDuration = DurationProperty.floatValue;
            _durationChanged = false;

            using (GUIFoldOut.Create(FoldOutProperty, () =>
            {
                // Header
                var btnTitle = GUILayout.Button("Animation", EditorStyles.boldLabel);
                var info = "";
                if (!FoldOut)
                {
                    if (AutoPlay != AutoPlayMode.None)
                    {
                        info += "| " + AutoPlay + " ";
                    }

                    if (PlayMode != PlayMode.Once)
                    {
                        info += "| " + PlayMode + " (" + PlayCount + ") ";
                    }

                    if (TimeMode != TimeMode.Normal)
                    {
                        info += "| " + TimeMode + " ";
                        ;
                    }

                    if (Math.Abs(SelfScale - 1f) > 1e-6)
                    {
                        info += "| " + SelfScale;
                    }
                }

                var btnFlexibleInfo = GUILayout.Button(info, EditorStyles.label, GUILayout.MinWidth(0), GUILayout.MaxWidth(Screen.width));
                if (btnTitle || btnFlexibleInfo)
                {
                    FoldOutProperty.boolValue = !FoldOutProperty.boolValue;
                }

                using (GUIEnableArea.Create(!IsInProgress))
                {
                    var btnContextMenu = GUIUtil.DrawContextMenuButton();
                    if (btnContextMenu)
                    {
                        var menu = CreateContextMenu();
                        menu.ShowAsContext();
                    }
                }
            }))
            {
                if (!FoldOut) return;

                // ID
                if (EnableIdentifier)
                {
                    EditorGUILayout.PropertyField(IdentifierProperty, new GUIContent("ID"));
                }

                using (GUIHorizontal.Create())
                {
                    using (var check = GUICheckChangeArea.Create())
                    {
                        var durationName = nameof(Duration);
                        if (SpeedBased) durationName = "Speed";
                        EditorGUILayout.PropertyField(DurationProperty, new GUIContent(durationName));
                        if (DurationProperty.floatValue < 0.001f) DurationProperty.floatValue = 0.001f;

                        if (check.Changed)
                        {
                            _durationChanged = true;
                        }
                    }

                    EditorGUILayout.PropertyField(DelayProperty, new GUIContent(nameof(Delay)));
                    if (DelayProperty.floatValue < 0) DelayProperty.floatValue = 0;
                }

                using (GUIHorizontal.Create())
                {
                    EditorGUILayout.PropertyField(PlayModeProperty, new GUIContent("Play"));
                    using (GUIEnableArea.Create(PlayMode != PlayMode.Once))
                    {
                        EditorGUILayout.PropertyField(PlayCountProperty, new GUIContent("Count"));
                        if (PlayMode == PlayMode.Once)
                        {
                            PlayCountProperty.intValue = 1;
                        }

                        if (PlayCount < -1) PlayCountProperty.intValue = -1;
                    }
                }

                using (GUIHorizontal.Create())
                {
                    EditorGUILayout.PropertyField(UpdateModeProperty, new GUIContent("Update"));

                    if (PlayMode == PlayMode.PingPong)
                    {
                        var rect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight, EditorStyles.label);
                        var width = rect.width;
                        rect.width = EditorStyle.LabelWidth;
                        GUI.Label(rect, nameof(Interval), EditorStyles.label);
                        rect.x += EditorStyle.LabelWidth + 2f;
                        rect.width = (width - rect.width - 6f) / 2f;
                        IntervalProperty.floatValue = EditorGUI.FloatField(rect, IntervalProperty.floatValue);
                        if (Interval < 0f) IntervalProperty.floatValue = 0f;
                        rect.x += rect.width + 3f;
                        Interval2Property.floatValue = EditorGUI.FloatField(rect, Interval2Property.floatValue);
                        if (Interval2 < 0f) IntervalProperty.floatValue = 0f;
                    }
                    else
                    {
                        using (GUIEnableArea.Create(PlayMode != PlayMode.Once))
                        {
                            EditorGUILayout.PropertyField(IntervalProperty);
                        }
                    }
                }

                using (GUIHorizontal.Create())
                {
                    EditorGUILayout.PropertyField(AutoPlayProperty, new GUIContent("Auto"));
                    EditorGUILayout.PropertyField(PreSampleModeProperty, new GUIContent("Sample"));
                }

                using (GUIHorizontal.Create())
                {
                    EditorGUILayout.PropertyField(TimeModeProperty, new GUIContent("Time"));
                    EditorGUILayout.PropertyField(SelfScaleProperty, new GUIContent("Scale"));
                }

                using (GUIHorizontal.Create())
                {
                    using (GUIEnableArea.Create(SingleMode))
                    {
                        GUIUtil.DrawToggleButton(SpeedBasedProperty);
                        if (!SingleMode)
                        {
                            SpeedBasedProperty.boolValue = false;
                        }
                    }

                    GUIUtil.DrawToggleButton(AutoKillProperty);
                }
            }
        }

        #endregion

        #region Draw Tweener List
       
        public virtual void DrawTweenerList()
        {
            if (TweenerList.Count == 0)
            {
                GUIUtil.DrawTipArea(UTweenEditorSetting.Ins.ErrorColor, "No Tweener");
                return;
            }

            for (var i = 0; i < TweenerList.Count; i++)
            {
                var tweener = TweenerList[i];
                if (tweener.Index != i || tweener.TweenerProperty == null)
                {
                    var tweenerProperty = TweenerListProperty.GetArrayElementAtIndex(i);
                    tweener.InitEditor(i, this, tweenerProperty);
                }

                // Sync tweeners duration and delay
                if (_durationChanged)
                {
                    var durationChangeRate = DurationProperty.floatValue / _originalDuration;
                    tweener.DelayProperty.floatValue *= durationChangeRate;
                    tweener.DurationProperty.floatValue *= durationChangeRate;
                }

                tweener.DrawTweener();
            }
        } 

        #endregion

        #region Draw Event

        public virtual void DrawEvent()
        {
            if (Mode != TweenEditorMode.Component) return;
            using (GUIFoldOut.Create(FoldOutEventProperty, "Event"))
            {
                if (!FoldOutEvent) return;
                using (GUIGroup.Create())
                {
                    var btnStyle = EditorStyles.toolbarButton;
                    using (GUIHorizontal.Create())
                    {
                        using (GUIVertical.Create())
                        {
                            if (GUILayout.Toggle(EventType == EventType.OnPlay, nameof(EventType.OnPlay), btnStyle))
                            {
                                OnPlay.InitEditor(TweenDataProperty, nameof(OnPlay));
                                EventType = EventType.OnPlay;
                            }

                            if (GUILayout.Toggle(EventType == EventType.OnLoopStart, nameof(EventType.OnLoopStart), btnStyle))
                            {
                                OnLoopStart.InitEditor(TweenDataProperty, nameof(OnLoopStart));
                                EventType = EventType.OnLoopStart;
                            }

                            if (GUILayout.Toggle(EventType == EventType.OnResume, nameof(EventType.OnResume), btnStyle))
                            {
                                OnResume.InitEditor(TweenDataProperty, nameof(OnResume));
                                EventType = EventType.OnResume;
                            }
                        }

                        using (GUIVertical.Create())
                        {
                            if (GUILayout.Toggle(EventType == EventType.OnStart, nameof(EventType.OnStart), btnStyle))
                            {
                                OnStart.InitEditor(TweenDataProperty, nameof(OnStart));
                                EventType = EventType.OnStart;
                            }

                            if (GUILayout.Toggle(EventType == EventType.OnLoopEnd, nameof(EventType.OnLoopEnd), btnStyle))
                            {
                                OnLoopEnd.InitEditor(TweenDataProperty, nameof(OnLoopEnd));
                                EventType = EventType.OnLoopEnd;
                            }

                            if (GUILayout.Toggle(EventType == EventType.OnStop, nameof(EventType.OnStop), btnStyle))
                            {
                                OnStop.InitEditor(TweenDataProperty, nameof(OnStop));
                                EventType = EventType.OnStop;
                            }
                        }

                        using (GUIVertical.Create())
                        {
                            if (GUILayout.Toggle(EventType == EventType.OnPause, nameof(EventType.OnPause), btnStyle))
                            {
                                OnPause.InitEditor(TweenDataProperty, nameof(OnPause));
                                EventType = EventType.OnPause;
                            }

                            if (GUILayout.Toggle(EventType == EventType.OnUpdate, nameof(EventType.OnUpdate), btnStyle))
                            {
                                OnUpdate.InitEditor(TweenDataProperty, nameof(OnUpdate));
                                EventType = EventType.OnUpdate;
                            }

                            if (GUILayout.Toggle(EventType == EventType.OnComplete, nameof(EventType.OnComplete), btnStyle))
                            {
                                OnComplete.InitEditor(TweenDataProperty, nameof(OnComplete));
                                EventType = EventType.OnComplete;
                            }
                        }
                    }
                }

                switch (EventType)
                {
                    case EventType.OnPlay:
                        OnPlay.DrawEvent(nameof(OnPlay));
                        break;
                    case EventType.OnStart:
                        OnStart.DrawEvent(nameof(OnStart));
                        break;
                    case EventType.OnUpdate:
                        OnUpdate.DrawEvent(nameof(OnUpdate));
                        break;
                    case EventType.OnLoopStart:
                        OnLoopStart.DrawEvent(nameof(OnLoopStart));
                        break;
                    case EventType.OnLoopEnd:
                        OnLoopEnd.DrawEvent(nameof(OnLoopEnd));
                        break;
                    case EventType.OnPause:
                        OnPause.DrawEvent(nameof(OnPause));
                        break;
                    case EventType.OnResume:
                        OnResume.DrawEvent(nameof(OnResume));
                        break;
                    case EventType.OnStop:
                        OnStop.DrawEvent(nameof(OnStop));
                        break;
                    case EventType.OnComplete:
                        OnComplete.DrawEvent(nameof(OnComplete));
                        break;
                }
            }
        }

        #endregion

        #region Draw Append
        
        public virtual void DrawAppend()
        {
            DrawAddTweener();
        }

        public virtual void DrawAddTweener()
        {
            using (GUIGroup.Create())
            {
                var buttonRect = EditorGUILayout.GetControlRect();
                var btnAddTweener = GUI.Button(buttonRect, "Add Tweener");
                if (btnAddTweener)
                {
                    var menu = GUIMenu.CreateTweenerMenu(tweenerType =>
                    {
                        var tweener = Activator.CreateInstance(tweenerType) as Tweener;
                        if (tweener == null) return;
                        Undo.RecordObject(SerializedObject.targetObject, "Add Tweener");
                        tweener.Reset();
                        tweener.InitParam(this, Mode == TweenEditorMode.Component ? MonoBehaviour : null);
                        TweenerList.Add(tweener);
                        tweener.OnAdded();
                    });
                    menu.Show(buttonRect);
                }
            }
        }

        #endregion

        #region Context Menu

        public virtual GenericMenu CreateContextMenu()
        {
            var menu = new GenericMenu();

            // Identifier
            menu.AddItem(EnableIdentifier ? "Disable Identifier" : "Enable Identifier", false, () =>
            {
                Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Switch Identifier");
                EnableIdentifier = !EnableIdentifier;
                if (!EnableIdentifier)
                {
                    Identifier = null;
                }
            });

            // Auto Sort
            if (TweenerList.Count > 1)
            {
                menu.AddItem(true, "Auto Sort", false, AutoSortTweener);
            }

            return menu;
        }

        public virtual void AutoSortTweener()
        {
            TweenerList.Sort((t1, t2) =>
            {
                if (Math.Abs(t1.Delay - t2.Delay) > 1e-3)
                {
                    return t1.Delay.CompareTo(t2.Delay);
                }
                else
                {
                    if (Math.Abs(t1.Duration - t2.Duration) > 1e-3)
                    {
                        return t1.Duration.CompareTo(t2.Duration);
                    }
                    else
                    {
                        if (t1.TweenerAttribute.Group != t2.TweenerAttribute.Group)
                        {
                            return string.Compare(t1.TweenerAttribute.Group, t2.TweenerAttribute.Group, StringComparison.Ordinal);
                        }
                        else
                        {
                            return string.Compare(t1.TweenerAttribute.DisplayName, t2.TweenerAttribute.DisplayName, StringComparison.Ordinal);
                        }
                    }
                }
            });
        }

        #endregion
    }
}
#endif