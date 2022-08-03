#if UTWEEN_TEXTMESHPRO
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    public static partial class UTweenTMP 
    {
        public const string IconPath = "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Text Component Icon.psd";
    }

#if UNITY_EDITOR

    public static partial class UTweenTMP
    {
        // [InitializeOnLoadMethod]
        // public static void InitEditor()
        // {
        //     EditorIcon.TweenerGroupIconDic.Add("TextMeshPro", EditorIcon.CreateIcon(IconPath));
        // }
    }

#endif
}

#endif