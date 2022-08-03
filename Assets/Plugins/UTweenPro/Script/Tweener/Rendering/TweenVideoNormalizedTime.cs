// using System;
// using UnityEngine;
// using UnityEngine.Video;
// #if UNITY_EDITOR
// using UnityEditor;
// #endif
//
// namespace Aya.TweenPro
// {
//     [Tweener("Video Normalized Time", "Rendering")]
//     [Serializable]
//     public partial class TweenVideoNormalizedTime : TweenValueFloat<VideoPlayer>
//     {
//         public override float Value
//         {
//             get => (float)(Target.time / Target.length);
//             set => Target.time = value * Target.length;
//         }
//     }
//
// #if UNITY_EDITOR
//
//     public partial class TweenVideoNormalizedTime : TweenValueFloat<VideoPlayer>
//     {
//         public override void DrawFromToValue()
//         {
//             base.DrawFromToValue();
//             From = Mathf.Clamp01(From);
//             To = Mathf.Clamp01(To);
//         }
//     }
//
// #endif
//
//     #region Extension
//
//     public static partial class UTween
//     {
//         public static TweenVideoNormalizedTime NormalizedTime(VideoPlayer videoPlayer, float to, float duration)
//         {
//             var tweener = Play<TweenVideoNormalizedTime, VideoPlayer, float>(videoPlayer, to, duration);
//             return tweener;
//         }
//
//         public static TweenVideoNormalizedTime NormalizedTime(VideoPlayer videoPlayer, float from, float to, float duration)
//         {
//             var tweener = Play<TweenVideoNormalizedTime, VideoPlayer, float>(videoPlayer, from, to, duration);
//             return tweener;
//         }
//     }
//
//     public static partial class VideoPlayerExtension
//     {
//         public static TweenVideoNormalizedTime TweenNormalizedTime(this VideoPlayer videoPlayer, float to, float duration)
//         {
//             var tweener = UTween.NormalizedTime(videoPlayer, to, duration);
//             return tweener;
//         }
//
//         public static TweenVideoNormalizedTime TweenNormalizedTime(this VideoPlayer videoPlayer, float from, float to, float duration)
//         {
//             var tweener = UTween.NormalizedTime(videoPlayer, from, to, duration);
//             return tweener;
//         }
//     }
//
//     #endregion
// }