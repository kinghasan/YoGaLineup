#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Aya.TweenPro
{
    public abstract partial class Tweener<TTarget, TValue> : Tweener<TTarget>
    where TTarget : UnityEngine.Object
    {
        [TweenerProperty, NonSerialized] public SerializedProperty FromProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty ToProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty AxisProperty;
        // [TweenerProperty, NonSerialized] public SerializedProperty OnUpdateEventProperty;

        [TweenerProperty, NonSerialized] internal SerializedProperty EnableAxisProperty = null;

        public virtual string AxisXName => nameof(AxisConstraint.X);
        public virtual string AxisYName => nameof(AxisConstraint.Y);
        public virtual string AxisZName => nameof(AxisConstraint.Z);
        public virtual string AxisWName => nameof(AxisConstraint.W);

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
        }

        public override void DrawIndependentAxis()
        {
            if (!EnableAxis) return;
            using (GUIHorizontal.Create())
            {
                GUILayout.Label(nameof(Axis), EditorStyles.label, GUILayout.Width(EditorGUIUtility.labelWidth));
                using (GUILabelWidthArea.Create(EditorStyle.CharacterWidth))
                {
                    var toggleAxises = new bool[4];
                    if (AxisCount >= 1) toggleAxises[0] = GUIUtil.DrawToggleButton(AxisXName, AxisX);
                    if (AxisCount >= 2) toggleAxises[1] = GUIUtil.DrawToggleButton(AxisYName, AxisY);
                    if (AxisCount >= 3) toggleAxises[2] = GUIUtil.DrawToggleButton(AxisZName, AxisZ);
                    if (AxisCount >= 4) toggleAxises[3] = GUIUtil.DrawToggleButton(AxisWName, AxisW);
                    var axis = 0;
                    for (var i = 0; i < 4; i++)
                    {
                        if (toggleAxises[i]) axis |= 2 << i;
                    }

                    AxisProperty.intValue = axis;
                }
            }
        }

        public override void DrawFromToValue()
        {
            using (GUIVertical.Create())
            {
                GUIUtil.DrawProperty(FromProperty);
                GUIUtil.DrawProperty(ToProperty);
            }
        }

        public override void DrawEvent()
        {

        }

        #region Context Menu

        public override GenericMenu CreateContextMenu()
        {
            var menu = base.CreateContextMenu();

            // Show / Hide Independent Axis
            if (SupportIndependentAxis)
            {
                menu.AddSeparator("");
                if (EnableAxis)
                {
                    menu.AddItem(new GUIContent("Disable Independent Axis"), false, () =>
                    {
                        Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Disable Independent Axis");
                        EnableAxisProperty.boolValue = !EnableAxisProperty.boolValue;
                        DisableIndependentAxis();
                        SerializedObject.ApplyModifiedProperties();
                    });
                }
                else
                {
                    menu.AddItem(new GUIContent("Enable Independent Axis"), false, () =>
                    {
                        Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Enable Independent Axis");
                        EnableAxisProperty.boolValue = !EnableAxisProperty.boolValue;
                        SerializedObject.ApplyModifiedProperties();
                    });
                }
            }

            // Reverse From To
            if (SupportFromTo)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Reverse From - To"), false, () =>
                {
                    Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Reverse From - To");
                    ReverseFromTo();
                });

                // Current - From / To
                if (SupportSetCurrentValue)
                {
                    menu.AddItem(Target != null, "Set Current -> From", false, () =>
                    {
                        Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Set Current -> From");
                        From = Value;
                    });
                    menu.AddItem(Target != null, "Set Current -> To", false, () =>
                    {
                        Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Set Current -> To");
                        To = Value;
                    });
                    menu.AddItem(Target != null, "Set From -> Current", false, () =>
                    {
                        Value = From;
                    });
                    menu.AddItem(Target != null, "Set To -> Current", false, () =>
                    {
                        Value = To;
                    });
                }
            }

            // Expand / Shrink 
            menu.AddSeparator("");
            menu.AddItem("Expand Others", false, () =>
            {
                Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Expand Others");
                foreach (var tweener in Data.TweenerList)
                {
                    if (tweener == this) continue;
                    tweener.FoldOut = true;
                }

                SerializedObject.ApplyModifiedProperties();
            });
            menu.AddItem("Narrow Others", false, () =>
            {
                Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Narrow Others");
                foreach (var tweener in Data.TweenerList)
                {
                    if (tweener == this) continue;
                    tweener.FoldOut = false;
                }

                SerializedObject.ApplyModifiedProperties();
            });

            // Active / DeActive
            menu.AddSeparator("");
            menu.AddItem("Active Others", false, () =>
            {
                Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "Active Others");
                foreach (var tweener in Data.TweenerList)
                {
                    if (tweener == this) continue;
                    tweener.Active = true;
                }

                SerializedObject.ApplyModifiedProperties();
            });
            menu.AddItem("DeActive Others", false, () =>
            {
                Undo.RegisterCompleteObjectUndo(SerializedObject.targetObject, "DeActive Others");
                foreach (var tweener in Data.TweenerList)
                {
                    if (tweener == this) continue;
                    tweener.Active = false;
                }

                SerializedObject.ApplyModifiedProperties();
            });

            return menu;
        }

        #endregion
    }
}
#endif