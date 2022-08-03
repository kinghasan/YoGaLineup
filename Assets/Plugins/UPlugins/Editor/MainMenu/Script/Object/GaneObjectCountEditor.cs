/////////////////////////////////////////////////////////////////////////////
//
//  Script : GaneObjectCountEditor.cs
//  Info   : 统计选中物体数量
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using Aya.Util;
using UnityEditor;
using UnityEngine;

namespace Aya.EditorScript
{
    public class GaneObjectCountEditor : MonoBehaviour
    {
        /// <summary>
        /// 统计选中物体数量
        /// </summary>
        [MenuItem(MenuUtil.MenuTitle + "Object/Selection Count", false)]
        private static void FindPrefabUsingInAllScenes()
        {
            Debug.Log("Selection Count ：" + Selection.gameObjects.Length);
        }
    }
}
#endif
