using System;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Audio Mixer Property", "Audio", "AudioMixerController Icon")]
    [Serializable]
    public partial class TweenAudioMixerProperty : TweenValueFloat<AudioMixer>
    {
        public string Property;

        public override float Value
        {
            get
            {
                Target.GetFloat(Property, out var value);
                return value;
            }
            set => Target.SetVolume(Property, value);
        }

        public override void Reset()
        {
            base.Reset();
            Property = "";
        }
    }

#if UNITY_EDITOR

    public partial class TweenAudioMixerProperty : TweenValueFloat<AudioMixer>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty PropertyProperty;

        public override void DrawFromToValue()
        {
            using (GUIErrorColorArea.Create(string.IsNullOrEmpty(Property)))
            {
                EditorGUILayout.PropertyField(PropertyProperty);
            }

            base.DrawFromToValue();
        }
    }

#endif

    #region Extension

    public partial class TweenAudioMixerProperty : TweenValueFloat<AudioMixer>
    {
        public TweenAudioMixerProperty SetProperty(string property)
        {
            Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAudioMixerProperty Property(AudioMixer audioMixer, string propertyName, float to, float duration)
        {
            var tweener = Create<TweenAudioMixerProperty>()
                .SetTarget(audioMixer)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenAudioMixerProperty;
            return tweener;
        }

        public static TweenAudioMixerProperty Property(AudioMixer audioMixer, string propertyName, float from, float to, float duration)
        {
            var tweener = Create<TweenAudioMixerProperty>()
                .SetTarget(audioMixer)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenAudioMixerProperty;
            return tweener;
        }
    }

    public static partial class AudioMixerExtension
    {
        public static TweenAudioMixerProperty TweenProperty(this AudioMixer audioMixer, string propertyName, float to, float duration)
        {
            var tweener = UTween.Property(audioMixer, propertyName, to, duration);
            return tweener;
        }

        public static TweenAudioMixerProperty TweenProperty(this AudioMixer audioMixer, string propertyName, float from, float to, float duration)
        {
            var tweener = UTween.Property(audioMixer, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}