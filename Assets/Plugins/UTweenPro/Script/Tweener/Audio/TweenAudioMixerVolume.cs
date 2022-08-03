using System;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Audio Mixer Volume", "Audio", "AudioMixerController Icon")]
    [Serializable]
    public partial class TweenAudioMixerVolume : TweenValueFloat<AudioMixer>
    {
        public string Group;

        public override float Value
        {
            get => Target.GetVolume(Group);
            set => Target.SetVolume(Group, value);
        }

        public override void Reset()
        {
            base.Reset();
            Group = "Master";
        }
    }

#if UNITY_EDITOR

    public partial class TweenAudioMixerVolume : TweenValueFloat<AudioMixer>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty GroupProperty;

        public override void DrawFromToValue()
        {
            using (GUIErrorColorArea.Create(string.IsNullOrEmpty(Group)))
            {
                EditorGUILayout.PropertyField(GroupProperty);
            }

            base.DrawFromToValue();
        }
    }

#endif

    #region Extension

    public partial class TweenAudioMixerVolume : TweenValueFloat<AudioMixer>
    {
        public TweenAudioMixerVolume SetGroup(string group)
        {
            Group = group;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAudioMixerVolume Volume(AudioMixer audioMixer, float to, float duration)
        {
            return Volume(audioMixer, "Master", to, duration);
        }

        public static TweenAudioMixerVolume Volume(AudioMixer audioMixer, string group, float to, float duration)
        {
            var tweener = Create<TweenAudioMixerVolume>()
                .SetTarget(audioMixer)
                .SetGroup(group)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenAudioMixerVolume;
            return tweener;
        }

        public static TweenAudioMixerVolume Volume(AudioMixer audioMixer, float from, float to, float duration)
        {
            return Volume(audioMixer, "Master", from, to, duration);
        }

        public static TweenAudioMixerVolume Volume(AudioMixer audioMixer, string group, float from, float to, float duration)
        {
            var tweener = Create<TweenAudioMixerVolume>()
                .SetTarget(audioMixer)
                .SetGroup(group)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenAudioMixerVolume;
            return tweener;
        }
    }

    public static partial class AudioMixerExtension
    {
        public static TweenAudioMixerVolume TweenVolume(this AudioMixer audioMixer, float to, float duration)
        {
            var tweener = UTween.Volume(audioMixer, to, duration);
            return tweener;
        }

        public static TweenAudioMixerVolume TweenVolume(this AudioMixer audioMixer, string group, float to, float duration)
        {
            var tweener = UTween.Volume(audioMixer, group, to, duration);
            return tweener;
        }

        public static TweenAudioMixerVolume TweenVolume(this AudioMixer audioMixer, float from, float to, float duration)
        {
            var tweener = UTween.Volume(audioMixer, from, to, duration);
            return tweener;
        }

        public static TweenAudioMixerVolume TweenVolume(this AudioMixer audioMixer, string group, float from, float to, float duration)
        {
            var tweener = UTween.Volume(audioMixer, group, from, to, duration);
            return tweener;
        }
    }

    #endregion
}