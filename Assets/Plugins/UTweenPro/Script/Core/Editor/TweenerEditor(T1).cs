#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Aya.TweenPro
{
    public abstract partial class Tweener<TTarget> : Tweener
    where TTarget : UnityEngine.Object
    {
        [TweenerProperty, NonSerialized] public SerializedProperty TargetProperty;

        [NonSerialized] public Texture TargetIcon;

        public override void InitParam(TweenData data, MonoBehaviour target)
        {
            base.InitParam(data, target);
            if (target == null) return;
            if (typeof(TTarget).IsSubclassOf(typeof(Component)))
            {
                Target = target.GetComponentInChildren<TTarget>(true);
            }
            else if (typeof(TTarget) == typeof(GameObject))
            {
                Target = (TTarget)(object)target.gameObject;
            }
        }

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
        }

        public override void DrawHeaderIcon()
        {
            if (TargetIcon == null)
            {
                TargetIcon = EditorIcon.GetTweenerIcon(GetType());
            }

            if (TargetIcon == null) return;
            var size = EditorStyle.TweenerHeaderIconSize;
            var btnIcon = GUILayout.Button("", EditorStyles.label, GUILayout.Width(size), GUILayout.Height(size));
            if (btnIcon)
            {
                FoldOut = !FoldOut;
            }

            var rect = GUILayoutUtility.GetLastRect();
            GUI.DrawTexture(rect, TargetIcon);
        }

        public override void DrawTitle()
        {
            base.DrawTitle();
            if (Target == null) return;

            using (GUIEnableArea.Create(Active, false))
            {
                var targetName = Target.name;
                GUILayout.Label("|", EditorStyles.label);
                GUILayout.Label(targetName, EditorStyle.TitleTargetLabel, GUILayout.MinWidth(0), GUILayout.MaxWidth(Screen.width));
            }
        }

        public override void DrawTarget()
        {
            if (!FoldOut) return;
            if (Data.Mode == TweenEditorMode.ScriptableObject) return;
            if (typeof(TTarget).IsSubclassOf(typeof(Component)) || typeof(TTarget) == typeof(Component))
            {
                GUIMenu.ComponentTreeMenu(typeof(TTarget), nameof(Target), TargetProperty, MonoBehaviour.transform, target =>
                {
                });
            }
            else
            {
                TargetProperty.objectReferenceValue = EditorGUILayout.ObjectField(nameof(Target), TargetProperty.objectReferenceValue, typeof(TTarget), true);
            }
        }
    }
}
#endif