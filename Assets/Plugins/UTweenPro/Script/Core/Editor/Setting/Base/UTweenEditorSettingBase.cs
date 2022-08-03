#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Aya.TweenPro
{
    public abstract class UTweenEditorSettingBase<T> : ScriptableObject
        where T : UTweenEditorSettingBase<T>
    {
        #region Instance

        public static T Ins
        {
            get
            {
                if (Instance == null) Instance = LoadAsset();
                return Instance;
            }
        }

        protected static T Instance;

        public static T LoadAsset()
        {
            var guidList = AssetDatabase.FindAssets("t:" + typeof(T).FullName);
            if (guidList != null && guidList.Length > 0)
            {
                var setting = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guidList[0]));
                setting.Init();
                return setting;
            }

            return null;
        }

        #endregion

        public virtual void Init()
        {

        }
    }
}
#endif