/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MeshRendererSortingOrderEditor.cs
//  Info     : 网格渲染器层级排序扩展
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
    /// <summary>
    /// MeshRendererのsortingLayer/sortingOrder拡張
    /// </summary>
    [CustomEditor(typeof(MeshRenderer)), CanEditMultipleObjects]
    public class MeshRendererSortingOrderEditor : Editor
    {
        private int[] _sortingLayerIds = null;
        private GUIContent[] _layerIdContents = null;

        /// <summary>
        /// 選択時の初期化処理
        /// </summary>
        private void OnEnable()
        {
            var sortingLayerNames = GetSortingLayerNames();
            _layerIdContents = new GUIContent[sortingLayerNames.Length];
            for (var i = 0; i < sortingLayerNames.Length; ++i)
                _layerIdContents[i] = new GUIContent(sortingLayerNames[i]);

            _sortingLayerIds = GetSortingLayerUniqueIDs();
        }

        /// <summary>
        /// Inspector表示
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var propSortingLayerID = serializedObject.FindProperty("m_SortingLayerID");
            var propSortingOrder = serializedObject.FindProperty("m_SortingOrder");

            EditorGUILayout.IntPopup(propSortingLayerID, _layerIdContents, _sortingLayerIds);
            EditorGUILayout.PropertyField(propSortingOrder);

            this.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// ソートレイヤー名取得
        /// </summary>
        private static string[] GetSortingLayerNames()
        {
            var internalEditorUtilityType = typeof(UnityEditorInternal.InternalEditorUtility);
            var sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return (string[])sortingLayersProperty.GetValue(null, null);
        }

        /// <summary>
        /// ソートレイヤーID取得
        /// </summary>
        private static int[] GetSortingLayerUniqueIDs()
        {
            var internalEditorUtilityType = typeof(UnityEditorInternal.InternalEditorUtility);
            var sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return (int[])sortingLayerUniqueIDsProperty.GetValue(null, null);
        }
    }
}

#endif