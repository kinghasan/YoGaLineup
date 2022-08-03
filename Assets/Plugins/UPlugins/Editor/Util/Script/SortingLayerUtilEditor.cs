/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SortingLayerUtilEditor.cs
//  Info     : SortingLayer 操作辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using UnityEditor;

namespace Aya.EditorScript
{
    public static class SortingLayerUtilEditor
    {
        /// <summary>
        /// 添加 SortingLayer
        /// </summary>
        /// <param name="sortingLayer">SortingLayer</param>
        public static void AddSortingLayer(string sortingLayer)
        {
            if (ContainsSortingLayer(sortingLayer)) return;
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name != "m_SortingLayers") continue;
                it.InsertArrayElementAtIndex(it.arraySize);
                var dataPoint = it.GetArrayElementAtIndex(it.arraySize - 1);
                while (dataPoint.NextVisible(true))
                {
                    if (dataPoint.name == "name")
                    {
                        dataPoint.stringValue = sortingLayer;
                        tagManager.ApplyModifiedProperties();
                    }
                    if (dataPoint.name == "uniqueID")
                    {
                        dataPoint.intValue = it.arraySize;
                        tagManager.ApplyModifiedProperties();
                    }
                }
                return;
            }
        }

        /// <summary>
        /// 是否包含 SortingLayer
        /// </summary>
        /// <param name="sortingLayer">SortingLayer</param>
        /// <returns>结果</returns>
        public static bool ContainsSortingLayer(string sortingLayer)
        {
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name != "m_SortingLayers") continue;
                for (var i = 0; i < it.arraySize; i++)
                {
                    var dataPoint = it.GetArrayElementAtIndex(i);
                    while (dataPoint.NextVisible(true))
                    {
                        if (dataPoint.name != "name") continue;
                        if (dataPoint.stringValue == sortingLayer) return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 清空所有 SortingLayer
        /// </summary>
        public static void ClearSortingLayer()
        {
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name != "m_SortingLayers") continue;
                it.ClearArray();
                tagManager.ApplyModifiedProperties();
                return;
            }
        }
    }
}
#endif