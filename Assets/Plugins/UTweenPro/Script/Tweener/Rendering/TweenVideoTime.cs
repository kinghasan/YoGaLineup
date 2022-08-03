// using System;
// using UnityEngine.Video;
//
// namespace Aya.TweenPro
// {
//     [Tweener("Video Time", "Rendering")]
//     [Serializable]
//     public class TweenVideoTime : TweenValueDouble<VideoPlayer>
//     {
//         public override double Value
//         {
//             get => Target.time;
//             set => Target.time = value;
//         }
//     }
//
//     #region Extension
//
//     public static partial class UTween
//     {
//         public static TweenVideoTime Time(VideoPlayer videoPlayer, double to, float duration)
//         {
//             var tweener = Play<TweenVideoTime, VideoPlayer, double>(videoPlayer, to, duration);
//             return tweener;
//         }
//
//         public static TweenVideoTime Time(VideoPlayer videoPlayer, double from, double to, float duration)
//         {
//             var tweener = Play<TweenVideoTime, VideoPlayer, double>(videoPlayer, from, to, duration);
//             return tweener;
//         }
//     }
//
//     public static partial class VideoPlayerExtension
//     {
//         public static TweenVideoTime TweenTime(this VideoPlayer spriteRenderer, double to, float duration)
//         {
//             var tweener = UTween.Time(spriteRenderer, to, duration);
//             return tweener;
//         }
//
//         public static TweenVideoTime TweenTime(this VideoPlayer spriteRenderer, double from, double to, float duration)
//         {
//             var tweener = UTween.Time(spriteRenderer, from, to, duration);
//             return tweener;
//         }
//     }
//
//     #endregion
// }