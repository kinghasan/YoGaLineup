/////////////////////////////////////////////////////////////////////////////
//
//  Script   : TrailRendererSortingOrderEditor.cs
//  Info     : 三角形渲染器层级排序扩展
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Internet
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Aya.EditorScript
{
    [CustomEditor(typeof(TrailRenderer)), CanEditMultipleObjects]
    public class TrailRendererSortingOrderEditor : Editor
    {
        private int[] sortingLayerIds = null;
        private GUIContent[] layerIDContents = null;
        /// <summary>
        /// 選択時の初期化処理
        /// </summary>
        private void OnEnable()
        {
            string[] sortingLayerNames = GetSortingLayerNames();
            this.layerIDContents = new GUIContent[sortingLayerNames.Length];
            for (int i = 0; i < sortingLayerNames.Length; ++i)
                this.layerIDContents[i] = new GUIContent(sortingLayerNames[i]);
            this.sortingLayerIds = GetSortingLayerUniqueIDs();
        }
        /// <summary>
        /// Inspector表示
        /// </summary>
        public override void OnInspectorGUI()
        {
            this.DrawDefaultInspector();
            SerializedProperty propSortingLayerID = this.serializedObject.FindProperty("m_SortingLayerID");
            SerializedProperty propSortingOrder = this.serializedObject.FindProperty("m_SortingOrder");
            EditorGUILayout.IntPopup(propSortingLayerID, this.layerIDContents, sortingLayerIds);
            EditorGUILayout.PropertyField(propSortingOrder);
            this.serializedObject.ApplyModifiedProperties();
        }
        /// <summary>
        /// ソートレイヤー名取得
        /// </summary>
        private static string[] GetSortingLayerNames()
        {
            System.Type internalEditorUtilityType = typeof(UnityEditorInternal.InternalEditorUtility);
            System.Reflection.PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return (string[])sortingLayersProperty.GetValue(null, null);
        }
        /// <summary>
        /// ソートレイヤーID取得
        /// </summary>
        private static int[] GetSortingLayerUniqueIDs()
        {
            System.Type internalEditorUtilityType = typeof(UnityEditorInternal.InternalEditorUtility);
            System.Reflection.PropertyInfo sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return (int[])sortingLayerUniqueIDsProperty.GetValue(null, null);
        }
    }
}
#endif