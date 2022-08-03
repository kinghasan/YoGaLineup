/////////////////////////////////////////////////////////////////////////////
//
//  Script : ShortCutToolsEditor.cs
//  Info   : 切换工具栏常用功能的快捷键
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio / Change from http://www.xuanyusong.com/archives/3900
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using Aya.Util;
using UnityEngine;
using UnityEditor;

namespace Aya.EditorScript
{
    public class ShortCutPlayerEditor : MonoBehaviour
    {
#if UNITY_EDITOR_WIN
        [MenuItem(MenuUtil.MenuTitle + "Short Cut/Player/Play丨Stop _F5", false)]
        private static void Play()
        {
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }

        [MenuItem(MenuUtil.MenuTitle + "Short Cut/Player/Pause丨Resume _F6", false)]
        private static void Stop()
        {
            EditorApplication.ExecuteMenuItem(EditorApplication.isPlaying ? "Edit/Pause" : "Edit/Play");
        }

        [MenuItem(MenuUtil.MenuTitle + "Short Cut/Player/Next Frame _F7", false)]
        private static void Step()
        {
            EditorApplication.ExecuteMenuItem("Edit/Step");
        }
#endif

#if UNITY_EDITOR_OSX
        [MenuItem(MenuUtil.MenuTitle + "hort Cut/Player/Play丨Stop _5", false)]
        private static void Play()
        {
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }

        [MenuItem(MenuUtil.MenuTitle + "Short Cut/Player/Pause丨Resume _6", false)]
        private static void Stop()
        {
            EditorApplication.ExecuteMenuItem(EditorApplication.isPlaying ? "Edit/Pause" : "Edit/Play");
        }

        [MenuItem(MenuUtil.MenuTitle + "Short Cut/Player/Next Frame _7", false)]
        private static void Step()
        {
            EditorApplication.ExecuteMenuItem("Edit/Step");
        }
#endif
    }
}
#endif