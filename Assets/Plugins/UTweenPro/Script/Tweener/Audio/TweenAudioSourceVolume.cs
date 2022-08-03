using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Audio Source Volume", "Audio")]
    [Serializable]
    public class TweenAudioSourceVolume : TweenValueFloat<AudioSource>
    {
        public override float Value
        {
            get => Target.volume;
            set => Target.volume = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenAudioSourceVolume Volume(AudioSource audioSource, float to, float duration)
        {
            var tweener = Play<TweenAudioSourceVolume, AudioSource, float>(audioSource, to, duration);
            return tweener;
        }

        public static TweenAudioSourceVolume Volume(AudioSource audioSource, float from, float to, float duration)
        {
            var tweener = Play<TweenAudioSourceVolume, AudioSource, float>(audioSource, from, to, duration);
            return tweener;
        }
    }

    public static partial class AudioSourceExtension
    {
        public static TweenAudioSourceVolume TweenVolume(this AudioSource audioSource, float to, float duration)
        {
            var tweener = UTween.Volume(audioSource, to, duration);
            return tweener;
        }

        public static TweenAudioSourceVolume TweenVolume(this AudioSource audioSource, float from, float to, float duration)
        {
            var tweener = UTween.Volume(audioSource, from, to, duration);
            return tweener;
        }
    }

    #endregion
}