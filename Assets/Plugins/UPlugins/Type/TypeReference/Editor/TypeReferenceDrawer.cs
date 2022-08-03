#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Aya.Types
{
    [CustomPropertyDrawer(typeof(TypeReference))]
    public class TypeReferenceDrawer : PropertyDrawer
    {
        public bool Enable;

        public TypeReferenceAttribute Attribute;
        public SerializedProperty AssemblyProperty;
        public SerializedProperty TypeProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Attribute == null)
            {
                Attribute = GetAttribute<TypeReferenceAttribute>(property, true);
                Enable = Attribute != null;
                AssemblyProperty = property.FindPropertyRelative("AssemblyName");
                TypeProperty = property.FindPropertyRelative("TypeName");
            }

            if (Attribute != null)
            {
                var labelWidth = EditorGUIUtility.labelWidth;
                var labelRect = position;
                labelRect.width = labelWidth;

                var filedRect = position;
                filedRect.x += labelWidth;
                filedRect.width -= labelWidth;

                TypeMenu(label.text, labelRect, filedRect, null);
            }
            else
            {
                var originalColor = GUI.color;
                GUI.color = Color.red;
                EditorGUI.LabelField(position, "No [Type Reference Attribute declared!", EditorStyles.helpBox);
                GUI.color = originalColor;
            }
        }

        protected void TypeMenu(string propertyName, Rect labelRect, Rect fieldRect, Action<Type> onClick = null)
        {
            var root = CreateTypeMenu();
            bool CheckNullFunc() => string.IsNullOrEmpty(TypeProperty.stringValue);
            void ResetFunc() => TypeProperty.stringValue = null;
            string CurrentDisplayNameGetter()
            {
                var type = TypeProperty.stringValue;
                return string.IsNullOrEmpty(type) ? "<None>" : type;
            }

            void OnClick(SearchableDropdownItem item)
            {
                var type = item.Value as Type;
                if (type != null)
                {
                    AssemblyProperty.stringValue = type.Assembly.FullName;
                    TypeProperty.stringValue = type.Name;
                }
                else
                {
                    AssemblyProperty.stringValue = null;
                    TypeProperty.stringValue = null;
                }

                AssemblyProperty.serializedObject.ApplyModifiedProperties();
                onClick?.Invoke(type);
            }

            SearchableDropdownList(propertyName, labelRect, fieldRect, root, CheckNullFunc, ResetFunc, CurrentDisplayNameGetter, OnClick);
        }

        protected SearchableDropdownItem CreateTypeMenu()
        {
            var root = new SearchableDropdownItem(Attribute.Type.Name);
            root.AddChild(new SearchableDropdownItem("<None>", null));
            root.AddSeparator();

            var typeCollection = TypeCache.GetTypesDerivedFrom(Attribute.Type);
            foreach (var type in typeCollection)
            {
                if (type.IsAbstract) continue;
                if (type.IsInterface) continue;
                if (type.IsGenericType) continue;
                if (type.IsEnum) continue;

                var child = new SearchableDropdownItem(type.Name, type);
                root.AddChild(child);
            }

            return root;
        }

        protected  void SearchableDropdownList(string propertyName, Rect labelRect, Rect fieldRect, SearchableDropdownItem root, Func<bool> checkNullFunc, Action resetFunc, Func<string> currentDisplayNameGetter, Action<SearchableDropdownItem> onClick = null)
        {
            var isNull = checkNullFunc();
            if (isNull)
            {
                resetFunc();
            }

            GUI.Label(labelRect, propertyName);
            var displayName = currentDisplayNameGetter();
            var btnType = GUI.Button(fieldRect, displayName, EditorStyles.popup);
            if (btnType)
            {
                fieldRect.width = EditorGUIUtility.currentViewWidth;
                var dropdown = new SearchableDropdown(root, onClick);
                dropdown.Show(fieldRect, 500f);
            }
        }



        protected T GetAttribute<T>(SerializedProperty serializedProperty, bool inherit) where T : Attribute
        {
            if (serializedProperty == null) { return null; }
            var type = serializedProperty.serializedObject.targetObject.GetType();
            FieldInfo field = null;
            PropertyInfo property = null;
            foreach (var name in serializedProperty.propertyPath.Split('.'))
            {
                field = type.GetField(name, (BindingFlags)(-1));
                if (field == null)
                {
                    property = type.GetProperty(name, (BindingFlags)(-1));
                    if (property == null)
                    {
                        return null;
                    }
                    type = property.PropertyType;
                }
                else
                {
                    type = field.FieldType;
                }
            }

            T[] attributes;

            if (field != null)
            {
                attributes = field.GetCustomAttributes(typeof(T), inherit) as T[];
            }
            else if (property != null)
            {
                attributes = property.GetCustomAttributes(typeof(T), inherit) as T[];
            }
            else
            {
                return null;
            }
            return attributes != null && attributes.Length > 0 ? attributes[0] : null;
        }
    }
}
#endif