/////////////////////////////////////////////////////////////////////////////
//
//  Script   : TagUtilEditor.cs
//  Info     : Tag 操作辅助类
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
    public static class TagUtilEditor
    {
        /// <summary>
        /// 添加 Tag
        /// </summary>
        /// <param name="tag">Tag</param>
        public static void AddTag(string tag)
        {
            if (ContainsTag(tag)) return;
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name != "tags") continue;
                it.InsertArrayElementAtIndex(it.arraySize);
                var dataPoint = it.GetArrayElementAtIndex(it.arraySize - 1);
                dataPoint.stringValue = tag;
                tagManager.ApplyModifiedProperties();
                return;
            }
        }

        /// <summary>
        /// 移除 Tag
        /// </summary>
        /// <param name="tag">Tag</param>
        public static void RemoveTag(string tag)
        {
            UnityEditorInternal.InternalEditorUtility.RemoveTag(tag);
        }

        /// <summary>
        /// 是否包含 Tag
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>结果</returns>
        public static bool ContainsTag(string tag)
        {
            for (var i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
            {
                if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(tag))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 清空所有 Tag
        /// </summary>
        public static void ClearTag()
        {
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name != "tags") continue;
                it.ClearArray();
                tagManager.ApplyModifiedProperties();
                return;
            }
        }
    }
}
#endif