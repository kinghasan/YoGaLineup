using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    public interface ITextCharacterModifier
    {
        Text GetTarget { get; }
        void Modify(int characterIndex, ref UIVertex[] vertices);
    }
}