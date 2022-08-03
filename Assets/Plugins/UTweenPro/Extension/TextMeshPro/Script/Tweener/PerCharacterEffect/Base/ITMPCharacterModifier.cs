#if UTWEEN_TEXTMESHPRO
using TMPro;
using UnityEngine;

namespace Aya.TweenPro
{
    public interface ITMPCharacterModifier
    {
        TMP_Text GetTarget { get; }
        bool ChangeGeometry { get; }
        bool ChangeColor { get; }

        void ModifyGeometry(int characterIndex, ref Vector3[] vertices, float progress);
        void ModifyColor(int characterIndex, ref Color32[] colors, float progress);
    }
}
#endif