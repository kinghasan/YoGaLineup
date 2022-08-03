#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aya.TweenPro
{
    public struct GUICheckChangeArea : IDisposable
    {
        private bool _end;
        private bool _changed;
        private Object _target;

        public bool Changed
        {
            get
            {
                if (_end) return _changed;
                _end = true;
                _changed = EditorGUI.EndChangeCheck();
                if (_changed && _target)
                {
                    Undo.RecordObject(_target, _target.name);
                }

                return _changed;
            }
        }

        public static GUICheckChangeArea Create(Object target = null)
        {
            EditorGUI.BeginChangeCheck();
            return new GUICheckChangeArea
            {
                _end = false,
                _changed = false,
                _target = target
            };
        }

        void IDisposable.Dispose()
        {
            if (_end) return;
            _end = true;
            _changed = EditorGUI.EndChangeCheck();
        }
    }

    public struct GUILabelWidthArea : IDisposable
    {
        public float OriginalLabelWidth;

        public static GUILabelWidthArea Create(float labelWidth)
        {
            return new GUILabelWidthArea(labelWidth);
        }

        private GUILabelWidthArea(float labelWidth)
        {
            OriginalLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = OriginalLabelWidth;
        }
    }

    public struct GUIVertical : IDisposable
    {
        public static GUIVertical Create(params GUILayoutOption[] options)
        {
            return new GUIVertical(options);
        }

        public static GUIVertical Create(GUIStyle style, params GUILayoutOption[] options)
        {
            return new GUIVertical(style, options);
        }

        private GUIVertical(params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options);
        }

        private GUIVertical(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
        }

        public void Dispose()
        {
            GUILayout.EndVertical();
        }
    }

    public struct GUIHorizontal : IDisposable
    {
        public static GUIHorizontal Create(params GUILayoutOption[] options)
        {
            return new GUIHorizontal(options);
        }

        public static GUIHorizontal Create(GUIStyle style, params GUILayoutOption[] options)
        {
            return new GUIHorizontal(style, options);
        }

        private GUIHorizontal(params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
        }

        private GUIHorizontal(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
        }

        public void Dispose()
        {
            GUILayout.EndHorizontal();
        }
    }

    public struct GUIErrorColorArea : IDisposable
    {
        public Color OriginalColor;

        public static GUIErrorColorArea Create(bool check = true)
        {
            return new GUIErrorColorArea(check);
        }

        public GUIErrorColorArea(bool check = true)
        {
            OriginalColor = GUI.color;
            if (check)
            {
                GUI.color = UTweenEditorSetting.Ins.ErrorColor;
            }
        }

        public void Dispose()
        {
            GUI.color = OriginalColor;
        }
    }

    public struct GUIEnableColorArea : IDisposable
    {
        public Color OriginalColor;

        public static GUIEnableColorArea Create(bool check = true)
        {
            return new GUIEnableColorArea(check);
        }

        public GUIEnableColorArea(bool check = true)
        {
            OriginalColor = GUI.color;
            if (check)
            {
                GUI.color = UTweenEditorSetting.Ins.EnableColor;
            }
            else
            {
                GUI.color = UTweenEditorSetting.Ins.DisableColor;
            }
        }

        public void Dispose()
        {
            GUI.color = OriginalColor;
        }
    }

    public struct GUIColorArea : IDisposable
    {
        public Color OriginalColor;

        public static GUIColorArea Create(Color color, bool check = true)
        {
            return new GUIColorArea(color, Color.white, check);
        }

        public static GUIColorArea Create(Color enableColor, Color disableColor, bool check = true)
        {
            return new GUIColorArea(enableColor, disableColor, check);
        }

        public GUIColorArea(Color enableColor, Color disableColor, bool check = true)
        {
            OriginalColor = GUI.color;
            if (check)
            {
                GUI.color = enableColor;
            }
            else
            {
                GUI.color = disableColor;
            }
        }

        public void Dispose()
        {
            GUI.color = OriginalColor;
        }
    }

    public struct GUIBackgroundColorArea : IDisposable
    {
        public Color OriginalColor;

        public static GUIBackgroundColorArea Create(Color color, bool check = true)
        {
            return new GUIBackgroundColorArea(color, Color.white, check);
        }

        public static GUIBackgroundColorArea Create(Color enableColor, Color disableColor, bool check = true)
        {
            return new GUIBackgroundColorArea(enableColor, disableColor, check);
        }

        public GUIBackgroundColorArea(Color enableColor, Color disableColor, bool check = true)
        {
            OriginalColor = GUI.backgroundColor;
            if (check)
            {
                GUI.backgroundColor = enableColor;
            }
            else
            {
                GUI.backgroundColor = disableColor;
            }
        }

        public void Dispose()
        {
            GUI.backgroundColor = OriginalColor;
        }
    }

    public struct GUIRectArea : IDisposable
    {
        public static GUIRectArea Create(Rect rect)
        {
            return new GUIRectArea(rect);
        }

        private GUIRectArea(Rect rect)
        {
            GUILayout.BeginArea(rect);
        }

        public void Dispose()
        {
            GUILayout.EndArea();
        }
    }

    public struct GUIGroup : IDisposable
    {
        public static GUIGroup Create(params GUILayoutOption[] options)
        {
            return new GUIGroup(options);
        }

        private GUIGroup(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, options);
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }

    public struct GUIWideMode : IDisposable
    {
        public bool OriginalMode;

        public static GUIWideMode Create(bool enable)
        {
            return new GUIWideMode(enable);
        }

        private GUIWideMode(bool enable)
        {
            OriginalMode = EditorGUIUtility.wideMode;
            EditorGUIUtility.wideMode = enable;
        }

        public void Dispose()
        {
            EditorGUIUtility.wideMode = OriginalMode;
        }
    }

    public struct GUIEnableArea : IDisposable
    {
        public bool OriginalEnable;

        public static GUIEnableArea Create(bool enable, bool inheritParent = true)
        {
            return new GUIEnableArea(enable, inheritParent);
        }

        private GUIEnableArea(bool enable, bool inheritParent = true)
        {
            OriginalEnable = GUI.enabled;
            if (!GUI.enabled && inheritParent) enable = false;
            GUI.enabled = enable;
        }

        public void Dispose()
        {
            GUI.enabled = OriginalEnable;
        }
    }

    public struct GUIFoldOut : IDisposable
    {
        public static GUIFoldOut Create(SerializedProperty foldOutProperty, string title = null, params GUILayoutOption[] options)
        {
            return new GUIFoldOut(foldOutProperty, () =>
            {
                var btnTitle = GUILayout.Button(title, EditorStyles.boldLabel);
                if (btnTitle)
                {
                    foldOutProperty.boolValue = !foldOutProperty.boolValue;
                }
            }, null, options);
        }

        public static GUIFoldOut Create(SerializedProperty foldOutProperty, Action drawTitleAction = null, params GUILayoutOption[] options)
        {
            return new GUIFoldOut(foldOutProperty, drawTitleAction, null, options);
        }

        public static GUIFoldOut Create(SerializedProperty foldOutProperty, Action drawTitleAction = null, Action drawAppendAction = null, params GUILayoutOption[] options)
        {
            return new GUIFoldOut(foldOutProperty, drawTitleAction, drawAppendAction, options);
        }

        private GUIFoldOut(SerializedProperty foldOutProperty, Action drawTitleAction, Action drawAppendAction = null, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, options);
            using (GUIHorizontal.Create())
            {
                foldOutProperty.boolValue = EditorGUILayout.Toggle(GUIContent.none, foldOutProperty.boolValue, EditorStyles.foldout, GUILayout.Width(EditorStyle.CharacterWidth));
                drawTitleAction?.Invoke();
            }

            drawAppendAction?.Invoke();
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
#endif