using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Audio Source Pitch", "Audio")]
    [Serializable]
    public class TweenAudioSourcePitch : TweenValueFloat<AudioSource>
    {
        public override float Value
        {
            get => Target.pitch;
            set => Target.pitch = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenAudioSourcePitch Pitch(AudioSource audioSource, float to, float duration)
        {
            var tweener = Play<TweenAudioSourcePitch, AudioSource, float>(audioSource, to, duration);
            return tweener;
        }

        public static TweenAudioSourcePitch Pitch(AudioSource audioSource, float from, float to, float duration)
        {
            var tweener = Play<TweenAudioSourcePitch, AudioSource, float>(audioSource, from, to, duration);
            return tweener;
        }
    }

    public static partial class AudioSourceExtension
    {
        public static TweenAudioSourcePitch TweenPitch(this AudioSource audioSource, float to, float duration)
        {
            var tweener = UTween.Pitch(audioSource, to, duration);
            return tweener;
        }

        public static TweenAudioSourcePitch TweenPitch(this AudioSource audioSource, float from, float to, float duration)
        {
            var tweener = UTween.Pitch(audioSource, from, to, duration);
            return tweener;
        }
    }

    #endregion
}