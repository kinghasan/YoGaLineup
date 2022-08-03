/////////////////////////////////////////////////////////////////////////////
//
//  Script : EditorPrefsEditor.cs
//  Info   : 编辑器存档数据操作
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using Aya.Util;
using UnityEngine;
using UnityEditor;

namespace Aya.EditorScript
{
    public class EditorPrefsEditor : MonoBehaviour
    {
        /// <summary>
        /// 清除清除EditorPrefs目录
        /// </summary>
        [MenuItem(MenuUtil.MenuTitle + "Save Data/Clear EditorPrefs (Editor Setting)", false)]
        static void DeleteAllData()
        {
            EditorPrefs.DeleteAll();
            Debug.Log("Delete all EditorPrefs success!");
        }
    }
}
#endif