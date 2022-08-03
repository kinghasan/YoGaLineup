#if UNITY_EDITOR
using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Serializable]
    public class TweenGroupData
    {
        public string Name;
        public Color Color;
        public string IconPath;

        public Texture2D Icon
        {
            get
            {
                if (_icon == null)
                {
                    if (!string.IsNullOrEmpty(IconPath))
                    {
                        _icon = EditorIcon.CreateIcon(IconPath);
                    }
                }

                return _icon;
            }
        }

        private Texture2D _icon;
    }
}
#endif