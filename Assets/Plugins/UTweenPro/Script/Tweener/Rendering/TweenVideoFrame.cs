// using System;
// using UnityEngine.Video;
//
// namespace Aya.TweenPro
// {
//     [Tweener("Video Frame", "Rendering")]
//     [Serializable]
//     public class TweenVideoFrame : TweenValueLong<VideoPlayer>
//     {
//         public override long Value
//         {
//             get => Target.frame;
//             set => Target.frame = value;
//         }
//     }
//
//     #region Extension
//
//     public static partial class UTween
//     {
//         public static TweenVideoFrame Frame(VideoPlayer videoPlayer, long to, float duration)
//         {
//             var tweener = Play<TweenVideoFrame, VideoPlayer, long>(videoPlayer, to, duration);
//             return tweener;
//         }
//
//         public static TweenVideoFrame Frame(VideoPlayer videoPlayer, long from, long to, float duration)
//         {
//             var tweener = Play<TweenVideoFrame, VideoPlayer, long>(videoPlayer, from, to, duration);
//             return tweener;
//         }
//     }
//
//     public static partial class VideoPlayerExtension
//     {
//         public static TweenVideoFrame TweenFrame(this VideoPlayer videoPlayer, long to, float duration)
//         {
//             var tweener = UTween.Frame(videoPlayer, to, duration);
//             return tweener;
//         }
//
//         public static TweenVideoFrame TweenFrame(this VideoPlayer videoPlayer, long from, long to, float duration)
//         {
//             var tweener = UTween.Frame(videoPlayer, from, to, duration);
//             return tweener;
//         }
//     }
//
//     #endregion
// }