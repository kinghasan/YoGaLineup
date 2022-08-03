#if UNITY_EDITOR
using UnityEditor;

namespace Aya.TweenPro
{
    public static class UTweenEditorSettingProvider
    {
        #region Preferences Setting

        [SettingsProvider]
        public static SettingsProvider GetTweenPreferencesSetting()
        {
            // var provider = AssetSettingsProvider.CreateProviderFromObject("Preferences/Aya Game Studio/UTween Pro", UTweenEditorSetting.Ins);
            // provider.keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(UTweenEditorSetting.Ins));
            
            var provider = new AssetSettingsProvider("Aya Game Studio/UTween Pro Editor", () => Editor.CreateEditor(UTweenEditorSetting.Ins));
            provider.keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(UTweenEditorSetting.Ins));
            return provider;
        }

        #endregion

        #region Project Setting

        [SettingsProvider]
        public static SettingsProvider GetTweenProjectSetting()
        {
            var provider = AssetSettingsProvider.CreateProviderFromObject("Aya Game Studio/UTween Pro", UTweenSetting.Ins);
            provider.keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(UTweenSetting.Ins));
            return provider;
        } 

        #endregion
    }
}

#endif