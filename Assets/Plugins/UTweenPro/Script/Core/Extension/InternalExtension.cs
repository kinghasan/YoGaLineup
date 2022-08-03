using UnityEngine;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    public static class InternalExtension
    {
        #region Renderer
        
        public static Material GetMaterial(this Renderer renderer, int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= renderer.sharedMaterials.Length) return null;
            return renderer.sharedMaterials[materialIndex];
        }

        #endregion

        #region Shader

        public static bool ContainsProperty(this Shader shader, string propertyName)
        {
            if (propertyName.EndsWith("_ST")) propertyName = propertyName.Replace("_ST", "");
            for (var i = 0; i < shader.GetPropertyCount(); i++)
            {
                var name = shader.GetPropertyName(i);
                if (name == propertyName) return true;
            }

            return false;
        }

        #endregion

        #region Gradient

        public static void Reverse(this Gradient gradient)
        {
            var alphaKeys = gradient.alphaKeys;
            var colorKeys = gradient.colorKeys;
            var newAlphaKeys = new GradientAlphaKey[alphaKeys.Length];
            var newColorKeys = new GradientColorKey[colorKeys.Length];
            for (var i = 0; i < alphaKeys.Length; i++)
            {
                var alphaKey = alphaKeys[i];
                newAlphaKeys[alphaKeys.Length - 1 - i] = new GradientAlphaKey(alphaKey.alpha, 1f - alphaKey.time);
            }

            for (var i = 0; i < colorKeys.Length; i++)
            {
                var colorKey = colorKeys[i];
                newColorKeys[colorKeys.Length - 1 - i] = new GradientColorKey(colorKey.color, 1f - colorKey.time);
            }

            gradient.SetKeys(newColorKeys, newAlphaKeys);
        }

        #endregion

        #region Vector3

        public static Vector3 Rotate(this Vector3 point, Vector3 center, Vector3 rotation)
        {
            var direction = point - center;
            direction = Quaternion.Euler(rotation) * direction + center;
            return direction;
        }

        #endregion

        #region AnimationCurve

        public static AnimationCurve Reverse(this AnimationCurve curve)
        {
            var keys = curve.keys;
            var newKeys = new Keyframe[keys.Length];

            for (var i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                newKeys[keys.Length - 1 - i] = new Keyframe(1f - key.time, key.value, -key.outTangent, -key.inTangent);
            }

            curve.keys = newKeys;
            return curve;
        }

        #endregion

        #region AudioMixer

        public static float AudioMixerMinVolume = -80f;
        public static float AudioMixerMaxVolume = 0f;

        public static AudioMixer SetVolume(this AudioMixer mixer, string groupName, float volume)
        {
            volume = Mathf.Clamp01(volume);
            mixer.SetFloat(groupName + "Volume", Mathf.Lerp(AudioMixerMinVolume, AudioMixerMaxVolume, volume));
            return mixer;
        }

        public static float GetVolume(this AudioMixer mixer, string groupName)
        {
            mixer.GetFloat(groupName + "Volume", out var volume);
            volume = (volume - AudioMixerMinVolume) / (AudioMixerMaxVolume - AudioMixerMinVolume);
            return volume;
        }

        #endregion

        #region Rect

        public static Rect Reduce(this Rect rect, float reduceSize)
        {
            var ret = new Rect(rect.x + reduceSize, rect.y + reduceSize, rect.width - reduceSize * 2, rect.height - reduceSize * 2);
            return ret;
        }

        public static Rect Expand(this Rect rect, float expandSize)
        {
            var ret = new Rect(rect.x - expandSize, rect.y - expandSize, rect.width + expandSize * 2, rect.height + expandSize * 2);
            return ret;
        }

        #endregion

#if UNITY_EDITOR

        #region GenericMenu Extension

        public static void AddItem(this GenericMenu menu, string content, bool on, GenericMenu.MenuFunction func)
        {
            AddItem(menu, true, content, on, func);
        }

        public static void AddItem(this GenericMenu menu, bool enable, string content, bool on, GenericMenu.MenuFunction func)
        {
            if (enable)
            {
                menu.AddItem(new GUIContent(content), on, func);
            }
            else
            {
                menu.AddDisabledItem(new GUIContent(content), on);
            }
        }

        #endregion

#endif
    }
}
