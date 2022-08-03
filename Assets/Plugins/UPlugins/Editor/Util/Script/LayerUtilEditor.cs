/////////////////////////////////////////////////////////////////////////////
//
//  Script   : LayerUtilEditor.cs
//  Info     : Layer 操作辅助类
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
    public static class LayerUtilEditor
    {
        /// <summary>
        /// 添加 Layer
        /// </summary>
        /// <param name="layer">Layer</param>
        public static void AddLayer(string layer)
        {
            if (ContainsLayer(layer)) return;
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name != "layers") continue;
                for (var i = 0; i < it.arraySize; i++)
                {
                    if (i == 3 || i == 6 || i == 7) continue;
                    var dataPoint = it.GetArrayElementAtIndex(i);
                    if (!string.IsNullOrEmpty(dataPoint.stringValue)) continue;
                    dataPoint.stringValue = layer;
                    tagManager.ApplyModifiedProperties();
                    return;
                }
            }
        }

        /// <summary>
        /// 是否包含 Layer
        /// </summary>
        /// <param name="layer">Layer</param>
        /// <returns>结果</returns>
        public static bool ContainsLayer(string layer)
        {
            for (var i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
            {
                if (UnityEditorInternal.InternalEditorUtility.layers[i].Contains(layer))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 晴空所有 Layer
        /// </summary>
        public static void ClearLayer()
        {
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name != "layers") continue;
                for (var i = 0; i < it.arraySize; i++)
                {
                    if (i == 3 || i == 6 || i == 7) continue;
                    var dataPoint = it.GetArrayElementAtIndex(i);
                    dataPoint.stringValue = string.Empty;
                }
                tagManager.ApplyModifiedProperties();
                return;
            }
        }

    }
}
#endif