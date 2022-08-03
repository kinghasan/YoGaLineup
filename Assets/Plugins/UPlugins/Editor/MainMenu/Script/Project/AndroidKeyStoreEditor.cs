/////////////////////////////////////////////////////////////////////////////
//
//  Script : AndroidKeyStoreEditor.cs
//  Info   : 配置安卓打包签名密钥
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
    [InitializeOnLoad]
    public class AndroidKeyStoreEditor : EditorWindow
    {
        private const string KEY_STORE_NAME_PREFS = "KEY_STORE_NAME_PREFS";
        private const string KEY_STORE_PASS_PREFS = "KEY_STORE_PASS_PREFS";
        private const string KEY_ALIAS_NAME_PREFS = "KEY_ALIAS_NAME_PREFS";
        private const string KEY_ALIAS_PASS_PREFS = "KEY_ALIAS_PASS_PREFS";

        public static string keystoreName;
        public static string keystorePass;

        public static string keyaliasName;
        public static string keyaliasPass;

        [MenuItem(MenuUtil.MenuTitle + "Project/Set Android Sign Keystore", false)]
        public static void ShowWindow()
        {
            var win = CreateInstance<AndroidKeyStoreEditor>();
            win.titleContent = new GUIContent("Set Android Sign Keystore");
            win.Show();
        }

        public void OnEnable()
        {
            keystoreName = EditorPrefs.GetString(Application.productName + KEY_STORE_NAME_PREFS, Application.dataPath + "/Assets/Plugins/NewSignKey.keystore");
            keystorePass = EditorPrefs.GetString(Application.productName + KEY_STORE_PASS_PREFS, "Gamelastic&5566");
            keyaliasName = EditorPrefs.GetString(Application.productName + KEY_ALIAS_NAME_PREFS, "gamelastic");
            keyaliasPass = EditorPrefs.GetString(Application.productName + KEY_ALIAS_PASS_PREFS, "Gamelastic&5566");
        }

        private static bool _alreadySign = false;
        [UnityEditor.Callbacks.PostProcessScene]
        public static void OnBuild()
        {
            if (!BuildPipeline.isBuildingPlayer)
            {
                return;
            }

            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                if (!_alreadySign)
                {
                    Save();
                    _alreadySign = true;
                }
            }
        }

        [UnityEditor.Callbacks.PostProcessBuild]
        public static void OnBuildOver(BuildTarget buildTarget, string output)
        {
            _alreadySign = false;
        }

        public static void Save()
        {
            EditorPrefs.SetString(Application.productName + KEY_STORE_NAME_PREFS, keystoreName);
            EditorPrefs.SetString(Application.productName + KEY_STORE_PASS_PREFS, keystorePass);
            EditorPrefs.SetString(Application.productName + KEY_ALIAS_NAME_PREFS, keyaliasName);
            EditorPrefs.SetString(Application.productName + KEY_ALIAS_PASS_PREFS, keyaliasPass);

            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = keystoreName;
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyaliasName;
            PlayerSettings.Android.keyaliasPass = keyaliasPass;
            Debug.Log($"Set Android Sign Keystore: {PlayerSettings.Android.keystoreName}," +
                      $"Password: {PlayerSettings.Android.keystorePass}，" +
                      $"Alias: {PlayerSettings.Android.keyaliasName}，" +
                      $"Alias Password: {PlayerSettings.Android.keyaliasPass}");
        }


        public void OnGUI()
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("Select Keystore File"))
            {
                keystoreName = EditorUtility.OpenFilePanel("Select Keystore File", Application.dataPath + "/../", "keystore");
                Save();
            }

            keystoreName = EditorGUILayout.TextField(new GUIContent("Keystore Name"), keystoreName);
            keystorePass = EditorGUILayout.TextField(new GUIContent("Keystore Password"), keystorePass);
            keyaliasName = EditorGUILayout.TextField(new GUIContent("Alias Name"), keyaliasName);
            keyaliasPass = EditorGUILayout.TextField(new GUIContent("Alias Password"), keyaliasPass);

            if (GUILayout.Button("Save"))
            {
                Save();
            }

            GUILayout.EndVertical();
        }
    }
}
#endif