using System;
using UnityEngine.Video;

namespace Aya.TweenPro
{
    [Tweener("Video Playback Speed", "Rendering")]
    [Serializable]
    public class TweenVideoPlaybackSpeed : TweenValueFloat<VideoPlayer>
    {
        public override float Value
        {
            get => Target.playbackSpeed;
            set => Target.playbackSpeed = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenVideoPlaybackSpeed PlaybackSpeed(VideoPlayer videoPlayer, float to, float duration)
        {
            var tweener = Play<TweenVideoPlaybackSpeed, VideoPlayer, float>(videoPlayer, to, duration);
            return tweener;
        }

        public static TweenVideoPlaybackSpeed PlaybackSpeed(VideoPlayer videoPlayer, float from, float to, float duration)
        {
            var tweener = Play<TweenVideoPlaybackSpeed, VideoPlayer, float>(videoPlayer, from, to, duration);
            return tweener;
        }
    }

    public static partial class VideoPlayerExtension
    {
        public static TweenVideoPlaybackSpeed TweenPlaybackSpeed(this VideoPlayer videoPlayer, float to, float duration)
        {
            var tweener = UTween.PlaybackSpeed(videoPlayer, to, duration);
            return tweener;
        }

        public static TweenVideoPlaybackSpeed TweenPlaybackSpeed(this VideoPlayer videoPlayer, float from, float to, float duration)
        {
            var tweener = UTween.PlaybackSpeed(videoPlayer, from, to, duration);
            return tweener;
        }
    }

    #endregion
}